using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Wave> _waves;       //A list of all waves in the game
    public int _currentWaveCount;       //The index of the current wave [Remember, a list starts from 0]

    [Header("Spawner Attributes")]
    float _spawnTimer; //timer use to determine whe to spawn the next enemy
    public int _enemiesAlive;
    public int _maxEnemiesAllowed;      //The maximum number of enemies allowed on tthe map at once
    public bool _maxEnemiesReached = false;     //a flag indicatin if the maximum number of enemies has been reached


    [Header("Spawn Positions")]
    public List<Transform> _basicSpawnPoints;        //A lst to store all the relative spawn points of enemies

    [SerializeField] private EnemyPool _pool;
    void Start() 
    {
        CalculateWaveQuota();
    }
    void Update() 
    {

        _spawnTimer += Time.deltaTime;

        //Check if it´s time to spawn the next enemy
        if(_spawnTimer >= _waves[_currentWaveCount]._spawnInterval)
        {
            _spawnTimer = 0f;
            SpawnEnemies();
            
        }
    }

    void SpawnEnemies()
    {
        //Check if the minimum number of enemies in the wave have been spawned
        if(_waves[_currentWaveCount]._spawnCount < _waves[_currentWaveCount]._waveQuota && !_maxEnemiesReached)
        {
            //Spawn each type of enemy until the quota is filled
            foreach (var enemyGroup in _waves[_currentWaveCount]._enemyGroups)
            {
                //Check if the minimum number of enemies of this type have been spwawned
                if(enemyGroup._spawnCount < enemyGroup._enemyCount)
                {
                    if(_enemiesAlive >= _maxEnemiesAllowed)
                    {
                        _maxEnemiesReached = true;
                        return;
                    }
                    
                    //Spawn the enemy at a random position close tothe player
                    GameObject enemy = _pool.RequestEnemy(enemyGroup.enemyPrefab);
                    enemy.GetComponent<EnemyBehaviour>().normalDir =  enemyGroup._enemyDirection;
                    enemy.transform.rotation =  enemyGroup._enemyDirection ? Quaternion.Euler(0f, 0f, 90): Quaternion.Euler(0f, 0f, 270);
                    Vector3 offset = enemyGroup._enemyDirection ? new Vector3 (11,0,0): new Vector3(-11,0,0);
                    enemy.transform.position = offset + _basicSpawnPoints[Random.Range(0,_basicSpawnPoints.Count)].position;
                    
                    //enemy.transform.parent = transform;

                    enemyGroup._spawnCount++;
                    _waves[_currentWaveCount]._spawnCount++;
                    _enemiesAlive++;
                }
            }
        }
        //Reset the maxEnemiesReached flag if the number of enemies alive has dropped below the maximum amount
        if(_enemiesAlive < _maxEnemiesAllowed)
        {
            _maxEnemiesReached = false;
        }
    }

    void CalculateWaveQuota()
    {
        int _currentWaveQuota = 0;
        foreach(var enemyGroup in _waves[_currentWaveCount]._enemyGroups)
        {
            _currentWaveQuota += enemyGroup._enemyCount;
        }

        _waves[_currentWaveCount]._waveQuota = _currentWaveQuota;
        Debug.Log("Cantidad de enemigos en esta oleada:" + _currentWaveQuota);
    }
}
