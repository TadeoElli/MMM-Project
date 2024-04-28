using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]private List<Wave> _waves;       //Una lista de todas las oleadas en el juego
    public Observer<int> _currentWaveCount = new Observer<int>(0);  //el indice de la oleada actual
    public Observer<int> _enemiesAlive = new Observer<int>(0);
    public Observer<int> _currentWaveQuota = new Observer<int>(0);
    [Header("Spawner Attributes")]
    float _spawnTimerForEnemies; //timer para determinar cuando spawnea el siguiente enemigo

    float _spawnTimerForGroup; //timer para determinar cuando spawnea el siguiente grupo
    [SerializeField]private int _maxEnemiesAllowed;      //Elk maximo de enemigos que puede haber activos en el mapa
    private bool hasToSpawnAGroup = true;
    [SerializeField] private float _waveInterval;

    [Header("Spawn Positions For Enemies")]
    [SerializeField]private  List<Transform> _spawnPoints;        //Una lista para guardar todos los spawn points para spawnear un enemigo

    [Header("Spawn Positions For Group of Enemies")]
    [SerializeField]private  List<GameObject> _waveSpawnPoints;        //Una lista para guardar objetos que formar grupos de spawn points para spawnear grupos de enemigos

    [SerializeField] private EnemyPool _pool;
    private float timer;
    void Start() 
    {
        CalculateWaveQuota();
        SpawnGroupOfEnemies();
        _spawnPoints = _waveSpawnPoints.SelectMany(point => point.GetComponentsInChildren<Transform>()).ToList();
        _currentWaveCount.Invoke();
    }
    void Update() 
    {
        timer += Time.deltaTime;
        _spawnTimerForEnemies += Time.deltaTime;
        _spawnTimerForGroup += Time.deltaTime;
        //Chequea si es tiempo para spawnear un siguiente enemigo grupo
        if(_spawnTimerForGroup >= _waves[_currentWaveCount.Value]._spawnIntervalForGroup && _waves[_currentWaveCount.Value]._spawnGroupCount < _waves[_currentWaveCount.Value]._waveGroupQuota){  
            //Si el timer supera el cooldown de un spawnGroup y la cuota de spawneo de grupos todavia no se cumplio
            hasToSpawnAGroup = true;
            _spawnTimerForGroup = 0f;
            SpawnGroupOfEnemies();      //Spawnea un grupo de enemigos
        }
        //Chequea si es tiempo para spawnear un siguiente enemigo
        if(_spawnTimerForEnemies >= _waves[_currentWaveCount.Value]._spawnIntervalForEnemy && !hasToSpawnAGroup)  //Si no tiene que spawnear un grupo y el 
        //timer supero el del intervalo entre spawn de enemigos
        {
            _spawnTimerForEnemies = 0f;
            SpawnSingleEnemies();     //Spawnea un enemigo
            
        }
        if(timer >= _waveInterval){
            StartCoroutine(BeginNextWave());
            timer = 0;
        }
    }

    IEnumerator BeginNextWave()
    {
        //Wave for "waveInterval" seconds before starting the next wave
        yield return new WaitForSeconds(3);

        //If there are more waves to start after the current wave, move on to the next wave
        if(_currentWaveCount.Value < _waves.Count -1)
        {
            _currentWaveCount.Value++;
            CalculateWaveQuota();
        }
    }
    void SpawnGroupOfEnemies()      //Spawnea un grupo de enemigos en alguna formacion al azar entre la lista de formaciones
    {
        if (_waves[_currentWaveCount.Value]._enemyGroups.Count == 0){
            hasToSpawnAGroup = false;
            return;
        }
        int groupIndex = Random.Range(0,_waveSpawnPoints.Count);
        List<Transform> spawnPoints = new List<Transform>();
        _waveSpawnPoints[groupIndex].GetComponentsInChildren<Transform>(false, spawnPoints);
       
        //Spawnea un grupo de enemigos spawneando uno en cada spawnpoint de la lista
        foreach (var spawn in spawnPoints)
        {
            if(_waves[_currentWaveCount.Value]._enemyGroups.Count == 0){
                hasToSpawnAGroup = false;
                _waves[_currentWaveCount.Value]._spawnGroupCount++;   //Aumenta la cantidad de grupos spawneados
                return;
            }else
            {
                int index = Random.Range(0,_waves[_currentWaveCount.Value]._enemyGroups.Count);   //Genera un numero aleatorio entre la cantidad de enemigos en la oleada
                GameObject enemy = CreateEnemy(index);  //Crea un enemigo
                Vector3 offset = _waves[_currentWaveCount.Value]._enemyGroups[index]._enemyDirection ? new Vector3 (13,0,0): new Vector3(-13,0,0);    //Aplica un offset para que aparezca fuera de camara
                enemy.transform.position = offset + spawn.position; //Lo coloca en la posicion del spawn point
                CheckEnemieGroupQuota(index);
            }
        }
        _waves[_currentWaveCount.Value]._spawnGroupCount++;   //Aumenta la cantidad de grupos spawneados
        hasToSpawnAGroup = false;
        _spawnTimerForEnemies = 0f;
        
    }
    void SpawnSingleEnemies()//Spawnea un solo enemigo aleatorio
    {
        if (_waves[_currentWaveCount.Value]._enemyGroups.Count == 0){
            return;
        }
        //Chequea si ya se supero la cuota de enemigos
        int index = Random.Range(0,_waves[_currentWaveCount.Value]._enemyGroups.Count);   //Genera un numero aleatorio entre la cantidad de enemigos en la oleada
        GameObject enemy = CreateEnemy(index);
        Vector3 offset = _waves[_currentWaveCount.Value]._enemyGroups[index]._enemyDirection ? new Vector3 (13,0,0): new Vector3(-13,0,0);    //Aplica un offset para que aparezca fuera de camara
        enemy.transform.position = offset + _spawnPoints[Random.Range(0,_spawnPoints.Count)].position;  
        CheckEnemieGroupQuota(index);
    }
    private GameObject CreateEnemy(int index){  //Creo un enemigo
        GameObject enemy = _pool.RequestEnemy(_waves[_currentWaveCount.Value]._enemyGroups[index].enemyPrefab);   //Spawnea un enemigo con ese indice
        enemy.GetComponent<EnemyBehaviour>().normalDir =  _waves[_currentWaveCount.Value]._enemyGroups[index]._enemyDirection;    //Setea su direccion con la direccion de la oleada
        enemy.transform.rotation =  _waves[_currentWaveCount.Value]._enemyGroups[index]._enemyDirection ? Quaternion.Euler(0f, 0f, 90): Quaternion.Euler(0f, 0f, 270);    //Setea su rotacion
        _waves[_currentWaveCount.Value]._enemyGroups[index]._spawnCount++;    //aumenta la cantidad de enemigos de un tipo spawneados
        //Si ya se cumplio la cuota de spawn de uno de los enemigos, lo quita de la lista para que no siga spawneando de ellos    
        _waves[_currentWaveCount.Value]._spawnEnemyCount++;     
        IncreaseEnemiesAlive();
        return enemy;
    }
    void CalculateWaveQuota()       //Calcula la cantidad de enemigos en la oleada
    {
        _currentWaveQuota.Value = _waves[_currentWaveCount.Value]._enemyGroups.CantOfEnemiesInWave();
        _currentWaveQuota.Value += _enemiesAlive.Value;
        Debug.Log("Cantidad de enemigos en esta oleada:" + _currentWaveQuota.Value);
    }
    private void CheckEnemieGroupQuota(int index){
        if(_waves[_currentWaveCount.Value]._enemyGroups[index]._spawnCount >= _waves[_currentWaveCount.Value]._enemyGroups[index]._enemyCount){
            _waves[_currentWaveCount.Value]._enemyGroups.Remove(_waves[_currentWaveCount.Value]._enemyGroups[index]);
        }    
    }
    public void ReduceEnemiesAlive(int amount){
        _enemiesAlive.Value -= amount;
        _currentWaveQuota.Value -= amount;
        GameManager.Instance.EnemiesAlive = _enemiesAlive.Value;
    }
    public void IncreaseEnemiesAlive(){
        _enemiesAlive.Value++;    //Aumenta el numero de enemigos vivos
        GameManager.Instance.EnemiesAlive = _enemiesAlive.Value;
    }
}
