using UnityEngine;
using System;
using System.Collections.Generic;

public class PowerUpManager : MonoBehaviour
{

    // Clase para definir los datos de un power-up
    [Serializable] 
    public class PowerUpData
    {
        [SerializeField]public PowerUp prefab;
        [SerializeField]public float dropProbability;
    }
    [Header("Will spawn the first prefab on the list")]
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
        for (int i = 0; i < availablePowerUps.Count; i++)
        {
            float randomValue = UnityEngine.Random.Range(0f, 100f);
            if(randomValue < availablePowerUps[i].dropProbability){
                return availablePowerUps[i].prefab.gameObject;
                break;
            }
        }            
        
        return null;
    }
}
