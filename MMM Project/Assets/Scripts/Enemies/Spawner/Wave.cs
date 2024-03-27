using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string _waveName;    //El nombre de la oleada
    public List<EnemyGroup> _enemyGroups;   //Una lista de los tipos de enemigos en la oleada
    public int _waveEnemyQuota;      //El total de enemigos que pueden spawnear en la oleada
    public int _waveGroupQuota;      //El total de grupos de enemigos que pueden spawnear en la oleada
    public float _spawnIntervalForEnemy;    //El intervalo en el que se spawnean nuevos enemigos
    public float _spawnIntervalForGroup;        //El intervalo en el que se spawnean nuevos grupos de enemigos
    public int _spawnEnemyCount;     //el numero de la cantidad de enemigos que ya spawnearon en la oleada
    public int _spawnGroupCount;     //el numero de la cantidad de grupos de enemigos que ya spawnearon en la oleada

}
