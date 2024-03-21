using UnityEngine;
using System;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour
{

    // Clase para definir los datos de un power-up
    [Serializable] 
    public class PowerUpData
    {
        [SerializeField]public GameObject prefab;
        [SerializeField]public float dropProbability;
    }

    [SerializeField] private List<PowerUpData> availablePowerUps;

    public void Death() {
        GameObject powerUp = GenerateRandomPowerUp();
        if(powerUp != null){
            GameObject newPowerUp = PowerUpPool.Instance.RequestPowerUp(powerUp);
            newPowerUp.transform.position = transform.position;
        }
    }

    // Genera y devuelve un power-up aleatorio
    private GameObject GenerateRandomPowerUp()
    {

        foreach (var powerUp in availablePowerUps)
        {
            // Generar un número aleatorio entre 0 y la suma total de las probabilidades
            float randomValue = UnityEngine.Random.Range(0f, 100f);
            if(randomValue < powerUp.dropProbability){
                return powerUp.prefab;
            }
        }
        return null;
    }
}
