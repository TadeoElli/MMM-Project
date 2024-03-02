using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool<T>
{
    Func<T> _FactoryMethod;

    List<T> _currentStock;

    Action<T> _TurnOnCallback;
    Action<T> _TurnOffCallback;

    public ObjectPool(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback, int initialAmount)
    {
        //Guardo COMO se crea el objeto
        _FactoryMethod = factoryMethod;

        //Guardo COMO inicializo el objeto al darselo al cliente
        _TurnOnCallback = turnOnCallback;

        //Guardo COMO apago el objeto para regresarlo a mi pool
        _TurnOffCallback = turnOffCallback;

        _currentStock = new List<T>(initialAmount);
        
        for (int i = 0; i < initialAmount; i++)
        {
            //Creo el objeto
            T obj = _FactoryMethod();

            //Lo apago
            _TurnOffCallback(obj);

            //Lo agrego a la lista
            _currentStock.Add(obj);
        }
    }

    public T GetObject()
    {
        //Creo la variable del objeto a devolver
        T obj = default;

        //Si mi lista NO esta vacia
        if (_currentStock.Count > 0)
        {
            //Obtengo el primer indice
            obj = _currentStock[0];

            //Lo saco de mi lista
            _currentStock.RemoveAt(0);
        }
        else //Si esta vacia
        {
            //Creo uno mas
            obj = _FactoryMethod();
        }

        //Lo prendo
        _TurnOnCallback(obj);

        //Lo devuelvo
        return obj;
    }

    public void ReturnObject(T obj)
    {
        //Lo apagamos
        _TurnOffCallback(obj);

        //Lo volvemos a guardar en la lista
        _currentStock.Add(obj);
    }
}
