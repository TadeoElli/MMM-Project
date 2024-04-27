using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;

public static class MyExtensions
{

    /*public static IEnumerable<Dst> SetCooldownsValue<Src, Dst>(this IEnumerable<Src> myCol, Func<Src, Dst> modifyElement)
    {
        for (int i = 1; i < yCol.Count(); i++)
        {
            var newElement = modifyElement(myCol[i]);
            yield return newElement;
        }
    }*/
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
}