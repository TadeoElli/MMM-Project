using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//IA2-LINQ
//Generators
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
/*
    //generator que recibe una lista de posiciones  y devuelve una tupla de esas posiciones aplicandoles un offset y un bool que indique la direccion que debe tomar el enemigo
    public static IEnumerable<(Transform, bool)> SetSpawnOffset(this IEnumerable<Transform> sourceList, Direction _direction)
    {
        bool directionBool = true; // Flag para indicar si es el primer elemento
        foreach (var item in sourceList)
        {
            // Modifica cada elemento de la lista de origen y sumo  el valor deseado
            switch (_direction)
            {
                case Direction.Left:
                    item.position = item.position + new Vector3(-13,0, 0);
                    yield return (item, false);
                    break;
                case Direction.Right:
                    item.position = item.position + new Vector3(13,0, 0);
                    yield return (item, true);
                    break;
                case Direction.Both:    //Si la direccion de la oleada es que vengan de ambos lados voy variando los offset por cada posicion
                    item.position = directionBool ? item.position + new Vector3(13,0, 0): item.position + new Vector3(-13,0, 0);
                    yield return (item, directionBool);
                    directionBool = !directionBool;
                    break;
                default:
                    //yield return default(Transform);
                    break;
            }
        }
    }
    //generator que resetea una lista de posiciones detectando de que lado del eje x se encuentra y restandole el offset deseado
    public static IEnumerable<Transform> ResetSpawnOffset(this IEnumerable<Transform> sourceList)
    {
        foreach (var item in sourceList)
        {
            // Se fija de que lado del eje x estaba el spawn y lo resetea
            item.position = item.position.x > 0 ? item.position + new Vector3(-13,0,0): item.position + new Vector3(13,0,0);
            yield return item;
        }
    }
    */
}