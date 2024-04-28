using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyExtensions
{

    //Generator que cambia los valores de una lista por otros restandoles una variable y que deja la primera posicion con valor default
    public static IEnumerable<Dst> SetCooldownsValue<Src, Dst>(this IEnumerable<Src> sourceList, Func<Src, Dst> modifyElement)
    {
        bool isFirstElement = true; // Flag para indicar si es el primer elemento
        foreach (var item in sourceList)
        {
            // Modifica cada elemento de la lista de origen y resta el valor deseado
            if (isFirstElement)
            {
                // Si es el primer elemento, establece su valor como 0
                yield return default(Dst);
                isFirstElement = false; // Cambia el flag para los siguientes elementos
            }
            else{
                Dst modifiedElement = modifyElement(item);
                yield return modifiedElement;   
            }
        }
    }
    //generator que selecciona una posicion aleatoria dentro de una lista de posiciones y le suma un offset dependiendo de un bool
    public static int CantOfEnemiesInWave(this IEnumerable<EnemyGroup> wave)
    {
        int _currentWaveQuota = 0;
        foreach(var enemyGroup in wave)
        {
            _currentWaveQuota += enemyGroup._enemyCount;
        }

        return _currentWaveQuota;
    }
}