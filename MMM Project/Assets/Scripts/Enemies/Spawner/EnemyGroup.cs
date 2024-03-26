using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyGroup
{
    public EnemyBehaviour enemyPrefab;
    public int _enemyCount;     //the number of enemies to spawn in this wav
    public int _spawnCount;     //The number of enemies already spawned in this wave
    public bool _enemyDirection;     //if is true, the enemies go to the left
}
