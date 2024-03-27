using UnityEngine;
using System;
using System.Collections.Generic;



public abstract class EnemyStrategy : ScriptableObject        //Strategy para todos los tipos de missiles
{
    [Serializable] 
    public class PowerUpData            // Clase que va a tener el power up y su respectiva probabilidad de dorp
    {
        [SerializeField]public PowerUp prefab;
        [SerializeField]public float dropProbability;
    }
    public float maxLife;       //Vida del enemigo
    public float collisionForce;        //Fuerza con la que empuja a las otras unidades al chocar
    public float velocity;      //Velocidad de movimiento   
    public Explosion explosion;     //La explosion que va a spawnear al morir

    [Header("Will spawn the first prefab on the list")]
    [SerializeField] public List<PowerUpData> availablePowerUps;        //Lista de todos los power up que puede dropear
    public abstract int CollisionBehaviour(GameObject other,EnemyBehaviour prefab);     //Comportamiento al collisionEnter
    
    public void CollisionForce(GameObject other, EnemyBehaviour prefab){        //Empuja al enemigo con el que choco segun la fuerza
        Rigidbody2D rb2D = other.GetComponent<Rigidbody2D>();

        Vector2 direction = other.transform.position - prefab.transform.position;
            
        rb2D.AddForce(direction.normalized * collisionForce, ForceMode2D.Force);
    }
    public abstract GameObject DeathBehaviour();        //Comportamiento al morir
    public void DropPowerUp(Transform origin) {       //Funcion que dropea el power Up
        PowerUp powerUp = GenerateRandomPowerUp();      //Llama a la funcion GeneratePowerUp y si devuelve algo lo guarda en la variable
        if(powerUp != null){
            GameObject newPowerUp = PowerUpPool.Instance.RequestPowerUp(powerUp);       //Si existe, lo instancia de la pool
            newPowerUp.transform.position = origin.position;
        }
    }
    public abstract void ParticleBehaviour(GameObject particle);        //Comportamiento con particulas si es que tiene
    public abstract void TriggerBehaviour(GameObject other);        //Comportamiento con TriggerEnter si es que tiene

    private PowerUp GenerateRandomPowerUp()     
    {
        for (int i = 0; i < availablePowerUps.Count; i++)   //Por Cada drop en la lista
        {
            float randomValue = UnityEngine.Random.Range(0f, 100f);     //genera un numero aleatorio
            if(randomValue < availablePowerUps[i].dropProbability){     //Si es menor a la probavilidad lo retorna, si no no retorna nada
                return availablePowerUps[i].prefab;
                break;
            }
        }            
        
        return null;
    }
    
}
