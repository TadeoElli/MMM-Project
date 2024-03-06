using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string _waveName;
    public List<EnemyGroup> _enemyGroups;   //A list of grups of enemies to spawn in this wave
    public int _waveQuota;      //The total number of enemies to spawn in this wave
    public float _spawnInterval;    //The interval at which to spawn enemies
    public int _spawnCount;     //The number of enemies already spawned in this wave

}
