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
    [SerializeField]private  List<(Transform, bool)> _spawnPoints;        //Una lista para guardar todos los spawn points para spawnear un enemigo


    [Header("Spawn Positions For Group of Enemies")]
    [SerializeField]private  List<GameObject> _waveSpawnPoints;        //Una lista para guardar objetos que formar grupos de spawn points para spawnear grupos de enemigos
    public List<List<Transform>> listOfTransformLists; // lista de listas de Transform
    [SerializeField] private EnemyPool _pool;
    private float timer;
    void Start() 
    {
        CalculateWaveQuota();
        //IA2-LINQ
        //Selecciono todos los transform de cada gameObject de la lista _waveSpawnPoints y los guardo en la lista de listas
        listOfTransformLists = _waveSpawnPoints.Select(obj => obj.transform.OfType<Transform>().ToList()).ToList();
        //Por cada lista en la lista de listas llamo al generator que setea el offset segun la direccion de donde vendran los enemigos de la oleada
        //y me devuelve una tupla que contiene la posicion con el offset y la direccion hacia donde se debe dirigir
        var transformedLists = listOfTransformLists.Select(item => item.SetSpawnOffset(_waves[_currentWaveCount.Value]._direction).ToList()).ToList();
        //Luego creo una lista que toma todos los transform de todas las listas de transforms para tener una que abarque todo
        //Esta lista servira para poder spawnear un enemigo en cualquier posicion de entre todas los spawnPoints de manera aleatoria
        _spawnPoints = transformedLists.SelectMany(point => point).ToList();
        

        SpawnGroupOfEnemies();
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
        //Espera 3 segundos para empezar la siguiente oleada
        yield return new WaitForSeconds(3);

        //Si hay mas oleadas despues de la actual, pasa a la siguiente oleada
        if(_currentWaveCount.Value < _waves.Count -1)
        {
            _currentWaveCount.Value++;
            CalculateWaveQuota();
            //IA2-LINQ
            //reseteo los valores de transform de los spawn con el generator de Reset y le aplico el nuevo offset con el generator setSpawnOffset
            var transformedLists = listOfTransformLists.Select(item => item.ResetSpawnOffset().SetSpawnOffset(_waves[_currentWaveCount.Value]._direction).ToList()).ToList();
            _spawnPoints = transformedLists.SelectMany(point => point).ToList();
        }
    }
    void SpawnGroupOfEnemies()      //Spawnea un grupo de enemigos en alguna formacion al azar entre la lista de formaciones
    {
        if (_waves[_currentWaveCount.Value]._enemyGroups.Count == 0){
            hasToSpawnAGroup = false;
            return;
        }
        int groupIndex = Random.Range(0,listOfTransformLists.Count);
        List<Transform> spawnPoints = listOfTransformLists[groupIndex];
       
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
                enemy.transform.position = spawn.position; //Lo coloca en la posicion del spawn point
                enemy.GetComponent<EnemyBehaviour>().normalDir = enemy.transform.position.x > 0 ? true : false;    //Setea su direccion con la direccion de la oleada
                enemy.transform.rotation =  enemy.transform.position.x < 0 ? Quaternion.Euler(0f, 0f, 270): Quaternion.Euler(0f, 0f, 90);    //Setea su rotacion 
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
        int spawnIndex = Random.Range(0,_spawnPoints.Count);
        enemy.transform.position = _spawnPoints[spawnIndex].Item1.position; //Utilizo el primer valor de la tupla para establecer su ´posicion
        enemy.GetComponent<EnemyBehaviour>().normalDir = _spawnPoints[spawnIndex].Item2;    //Utilizo el segundo valor de la tupla para establecer su direccion
        enemy.transform.rotation =  enemy.transform.position.x < 0 ? Quaternion.Euler(0f, 0f, 270): Quaternion.Euler(0f, 0f, 90);    //Setea su rotacion  
        CheckEnemieGroupQuota(index);
    }
    private GameObject CreateEnemy(int index){  //Creo un enemigo
        GameObject enemy = _pool.RequestEnemy(_waves[_currentWaveCount.Value]._enemyGroups[index].enemyPrefab);   //Spawnea un enemigo con ese indice
        //enemy.GetComponent<EnemyBehaviour>().normalDir = enemy.transform.position.x > 0 ? true : false;    //Setea su direccion con la direccion de la oleada
        //enemy.transform.rotation =  enemy.transform.position.x > 0 ? Quaternion.Euler(0f, 0f, 270): Quaternion.Euler(0f, 0f, 90);    //Setea su rotacion
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
