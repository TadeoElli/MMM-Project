using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string _waveName;
    public List<EnemyGroup> _enemyGroups;   //A list of grups of enemies to spawn in this wave
    public int _waveEnemyQuota;      //The total number of enemies to spawn in this wave
    public int _waveGroupQuota;
    public float _spawnIntervalForEnemy;    //The interval at which to spawn enemies random
    public float _spawnIntervalForGroup;        //The interval at wich spawn a list of enemies in group
    public int _spawnEnemyCount;     //The number of enemies already spawned in this wave
    public int _spawnGroupCount;

}
