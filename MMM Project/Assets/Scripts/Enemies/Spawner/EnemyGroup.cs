using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyGroup
{
    public EnemyBehaviour enemyPrefab;  
    public int _enemyCount;     //El numero de enemigos que pueden spawnear en la oleada de este tipo
    public int _spawnCount;     //el numero de enemigos que ya spawnearon en la oleada de este tipo
    public bool _enemyDirection;     //Laa direccion en la que van a ir, si es True, van hacia la izquierda
}
