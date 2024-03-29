using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]private List<Wave> _waves;       //Una lista de todas las oleadas en el juego
    [SerializeField]private int _currentWaveCount;       //el indice de la oleada actual

    [Header("Spawner Attributes")]
    float _spawnTimerForEnemies; //timer para determinar cuando spawnea el siguiente enemigo

    float _spawnTimerForGroup; //timer para determinar cuando spawnea el siguiente grupo
    [SerializeField]private int _enemiesAlive;
    [SerializeField]private int _maxEnemiesAllowed;      //Elk maximo de enemigos que puede haber activos en el mapa
    private bool hasToSpawnAGroup = true;


    [Header("Spawn Positions For Enemies")]
    [SerializeField]private  List<Transform> _basicSpawnPoints;        //Una lista para guardar todos los spawn points para spawnear un enemigo

    [Header("Spawn Positions For Group of Enemies")]
    [SerializeField]private  List<GameObject> _prefabSpawnPoints;        //Una lista para guardar objetos que formar grupos de spawn points para spawnear grupos de enemigos

    [SerializeField] private EnemyPool _pool;
    void Start() 
    {
        CalculateWaveQuota();
        SpawnGroupOfEnemies();
    }
    void Update() 
    {

        _spawnTimerForEnemies += Time.deltaTime;
        _spawnTimerForGroup += Time.deltaTime;
        //Chequea si es tiempo para spawnear un siguiente enemigo grupo
        if(_spawnTimerForGroup >= _waves[_currentWaveCount]._spawnIntervalForGroup && _waves[_currentWaveCount]._spawnGroupCount < _waves[_currentWaveCount]._waveGroupQuota){  
            //Si el timer supera el cooldown de un spawnGroup y la cuota de spawneo de grupos todavia no se cumplio
            hasToSpawnAGroup = true;
            _spawnTimerForGroup = 0f;
            SpawnGroupOfEnemies();      //Spawnea un grupo de enemigos
        }
        //Chequea si es tiempo para spawnear un siguiente enemigo
        if(_spawnTimerForEnemies >= _waves[_currentWaveCount]._spawnIntervalForEnemy && !hasToSpawnAGroup)  //Si no tiene que spawnear un grupo y el 
        //timer supero el del intervalo entre spawn de enemigos
        {
            _spawnTimerForEnemies = 0f;
            SpawnSingleEnemies();     //Spawnea un enemigo
            
        }
    }

    void SpawnGroupOfEnemies()      //Spawnea un grupo de enemigos en alguna formacion al azar entre la lista de formaciones
    {
        int groupIndex = Random.Range(0,_prefabSpawnPoints.Count);
        List<Transform> spawnPoints = new List<Transform>();
        _prefabSpawnPoints[groupIndex].GetComponentsInChildren<Transform>(false, spawnPoints);
        //Chequea si ya se supero la cuota de grupos spawneados
        if(_waves[_currentWaveCount]._spawnGroupCount < _waves[_currentWaveCount]._waveGroupQuota)
        {
            //Spawnea un grupo de enemigos spawneando uno en cada spawnpoint de la lista
            foreach (var spawn in spawnPoints)
            {
                int index = Random.Range(0,_waves[_currentWaveCount]._enemyGroups.Count);   //Genera un numero aleatorio entre la cantidad de enemigos en la oleada
                GameObject enemy = CreateEnemy(index);  //Crea un enemigo
                Vector3 offset = _waves[_currentWaveCount]._enemyGroups[index]._enemyDirection ? new Vector3 (13,0,0): new Vector3(-13,0,0);    //Aplica un offset para que aparezca fuera de camara
                enemy.transform.position = offset + spawn.position; //Lo coloca en la posicion del spawn point
            }
            _waves[_currentWaveCount]._spawnGroupCount++;   //Aumenta la cantidad de grupos spawneados
            hasToSpawnAGroup = false;
            _spawnTimerForEnemies = 0f;
        }
    }
    void SpawnSingleEnemies()//Spawnea un solo enemigo aleatorio
    {
        //Chequea si ya se supero la cuota de enemigos
        if(_waves[_currentWaveCount]._spawnEnemyCount < _waves[_currentWaveCount]._waveEnemyQuota)
        {
            int index = Random.Range(0,_waves[_currentWaveCount]._enemyGroups.Count);   //Genera un numero aleatorio entre la cantidad de enemigos en la oleada
            GameObject enemy = CreateEnemy(index);
            Vector3 offset = _waves[_currentWaveCount]._enemyGroups[index]._enemyDirection ? new Vector3 (13,0,0): new Vector3(-13,0,0);    //Aplica un offset para que aparezca fuera de camara
            enemy.transform.position = offset + _basicSpawnPoints[Random.Range(0,_basicSpawnPoints.Count)].position;  
        }

    }
    private GameObject CreateEnemy(int index){  //Creo un enemigo
        GameObject enemy = _pool.RequestEnemy(_waves[_currentWaveCount]._enemyGroups[index].enemyPrefab);   //Spawnea un enemigo con ese indice
        enemy.GetComponent<EnemyBehaviour>().normalDir =  _waves[_currentWaveCount]._enemyGroups[index]._enemyDirection;    //Setea su direccion con la direccion de la oleada
        enemy.transform.rotation =  _waves[_currentWaveCount]._enemyGroups[index]._enemyDirection ? Quaternion.Euler(0f, 0f, 90): Quaternion.Euler(0f, 0f, 270);    //Setea su rotacion
        _waves[_currentWaveCount]._enemyGroups[index]._spawnCount++;    //aumenta la cantidad de enemigos de un tipo spawneados
        //Si ya se cumplio la cuota de spawn de uno de los enemigos, lo quita de la lista para que no siga spawneando de ellos
        if(_waves[_currentWaveCount]._enemyGroups[index]._spawnCount >= _waves[_currentWaveCount]._enemyGroups[index]._enemyCount){
            _waves[_currentWaveCount]._enemyGroups.Remove(_waves[_currentWaveCount]._enemyGroups[index]);
        }             
        _enemiesAlive++;    //Aumenta el numero de enemigos vivos
        return enemy;
    }
    void CalculateWaveQuota()       //Calcula la cantidad de enemigos en la oleada
    {
        int _currentWaveQuota = 0;
        foreach(var enemyGroup in _waves[_currentWaveCount]._enemyGroups)
        {
            _currentWaveQuota += enemyGroup._enemyCount;
        }

        _waves[_currentWaveCount]._waveEnemyQuota = _currentWaveQuota;
        Debug.Log("Cantidad de enemigos en esta oleada:" + _currentWaveQuota);
    }

    private void ChangeCurrentWave(int wave){
        _currentWaveCount = wave;
        CalculateWaveQuota();
    }
}
