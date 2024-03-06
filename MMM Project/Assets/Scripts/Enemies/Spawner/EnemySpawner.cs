using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]private List<Wave> _waves;       //A list of all waves in the game
    [SerializeField]private int _currentWaveCount;       //The index of the current wave [Remember, a list starts from 0]

    [Header("Spawner Attributes")]
    float _spawnTimerForEnemies; //timer use to determine whe to spawn the next enemy

    float _spawnTimerForGroup; //timer use to determine whe to spawn the next group
    [SerializeField]private int _enemiesAlive;
    [SerializeField]private int _maxEnemiesAllowed;      //The maximum number of enemies allowed on tthe map at once
    private bool hasToSpawnAGroup = true;


    [Header("Spawn Positions For Enemies")]
    [SerializeField]private  List<Transform> _basicSpawnPoints;        //A lst to store all the relative spawn points of enemies

    [Header("Spawn Positions For Group of Enemies")]
    [SerializeField]private  List<GameObject> _prefabSpawnPoints;        //A lst to store all the relative spawn points of enemies

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

        if(_spawnTimerForGroup >= _waves[_currentWaveCount]._spawnIntervalForGroup){
            hasToSpawnAGroup = true;
            _spawnTimerForGroup = 0f;
            SpawnGroupOfEnemies();
        }
        //Check if it´s time to spawn the next enemy
        if(_spawnTimerForEnemies >= _waves[_currentWaveCount]._spawnIntervalForEnemy && !hasToSpawnAGroup)
        {
            _spawnTimerForEnemies = 0f;
            SpawnEnemies();
            
        }
    }

    void SpawnGroupOfEnemies()
    {
        int groupIndex = Random.Range(0,_prefabSpawnPoints.Count);
        List<Transform> spawnPoints = new List<Transform>();
        _prefabSpawnPoints[groupIndex].GetComponentsInChildren<Transform>(false, spawnPoints);
        //Check if the minimum number of enemies in the wave have been spawned
        if(_waves[_currentWaveCount]._spawnGroupCount < _waves[_currentWaveCount]._waveGroupQuota)
        {
            //Spawn each type of enemy until the quota is filled
            foreach (var spawn in spawnPoints)
            {
                int index = Random.Range(0,_waves[_currentWaveCount]._enemyGroups.Count);
                GameObject enemy = _pool.RequestEnemy(_waves[_currentWaveCount]._enemyGroups[index].enemyPrefab);
                enemy.GetComponent<EnemyBehaviour>().normalDir =  _waves[_currentWaveCount]._enemyGroups[index]._enemyDirection;
                enemy.transform.rotation =  _waves[_currentWaveCount]._enemyGroups[index]._enemyDirection ? Quaternion.Euler(0f, 0f, 90): Quaternion.Euler(0f, 0f, 270);
                Vector3 offset = _waves[_currentWaveCount]._enemyGroups[index]._enemyDirection ? new Vector3 (12,0,0): new Vector3(-12,0,0);
                enemy.transform.position = offset + spawn.position; 
                _waves[_currentWaveCount]._enemyGroups[index]._spawnCount++;
                if(_waves[_currentWaveCount]._enemyGroups[index]._spawnCount >= _waves[_currentWaveCount]._enemyGroups[index]._enemyCount){
                    _waves[_currentWaveCount]._enemyGroups.Remove(_waves[_currentWaveCount]._enemyGroups[index]);
                }             
                _enemiesAlive++;
            }
            _waves[_currentWaveCount]._spawnGroupCount++;
            hasToSpawnAGroup = false;
            _spawnTimerForEnemies = 0f;
        }
    }
    void SpawnEnemies()
    {
        //Check if the minimum number of enemies in the wave have been spawned
        if(_waves[_currentWaveCount]._spawnEnemyCount < _waves[_currentWaveCount]._waveEnemyQuota)
        {
            //Spawn each type of enemy until the quota is filled
            int index = Random.Range(0,_waves[_currentWaveCount]._enemyGroups.Count);
            //Check if the minimum number of enemies of this type have been spwawned
            //Spawn the enemy at a random position close tothe player
            GameObject enemy = _pool.RequestEnemy(_waves[_currentWaveCount]._enemyGroups[index].enemyPrefab);
            enemy.GetComponent<EnemyBehaviour>().normalDir =  _waves[_currentWaveCount]._enemyGroups[index]._enemyDirection;
            enemy.transform.rotation =  _waves[_currentWaveCount]._enemyGroups[index]._enemyDirection ? Quaternion.Euler(0f, 0f, 90): Quaternion.Euler(0f, 0f, 270);
            Vector3 offset = _waves[_currentWaveCount]._enemyGroups[index]._enemyDirection ? new Vector3 (12,0,0): new Vector3(-12,0,0);
            enemy.transform.position = offset + _basicSpawnPoints[Random.Range(0,_basicSpawnPoints.Count)].position;
            
            _waves[_currentWaveCount]._enemyGroups[index]._spawnCount++;
            if(_waves[_currentWaveCount]._enemyGroups[index]._spawnCount >= _waves[_currentWaveCount]._enemyGroups[index]._enemyCount){
                _waves[_currentWaveCount]._enemyGroups.Remove(_waves[_currentWaveCount]._enemyGroups[index]);
            }
            _waves[_currentWaveCount]._spawnEnemyCount++;
            _enemiesAlive++;
            
        }

    }

    void CalculateWaveQuota()
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
