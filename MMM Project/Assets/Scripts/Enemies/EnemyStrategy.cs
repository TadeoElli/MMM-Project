using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;



public abstract class EnemyStrategy : ScriptableObject        //Strategy para todos los tipos de missiles
{
    #region Variables
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
    public int score;   //El puntaje que otroga al morir
    public int experience;  //la cantidad de experiencia que otorga al morir

    [Header("Will spawn the first prefab on the list")]
    [SerializeField] private List<PowerUpData> availablePowerUps;        //Lista de todos los power up que puede dropear

    [Header("Sounds Effect")]
    public AudioClip bounceClip;        //Lista de todos los power up que puede dropear
    #endregion
    #region Funciones
    public abstract int CollisionBehaviour(GameObject other,EnemyBehaviour prefab);     //Comportamiento al collisionEnter
    
    public void CollisionForce(GameObject other, EnemyBehaviour prefab){        //Empuja al enemigo con el que choco segun la fuerza
        Rigidbody2D rb2D = other.GetComponent<Rigidbody2D>();

        Vector2 direction = other.transform.position - prefab.transform.position;
            
        rb2D.AddForce(direction.normalized * collisionForce, ForceMode2D.Force);
    }

    public virtual GameObject DeathBehaviour()        //Comportamiento al morir
    {
        return ExplosionPool.Instance.RequestExplosion(explosion); // Devuelve la explosión basica
    }
    public void DropPowerUp(Transform origin) {       //Funcion que dropea el power Up
        PowerUp powerUp = GenerateRandomPowerUp();      //Llama a la funcion GeneratePowerUp y si devuelve algo lo guarda en la variable
        if(powerUp != null){
            GameObject newPowerUp = PowerUpPool.Instance.RequestPowerUp(powerUp);       //Si existe, lo instancia de la pool
            newPowerUp.transform.position = origin.position;
        }
    }

    public virtual void ParticleBehaviour(GameObject particle)        //Comportamiento con particulas si es que tiene
    {
        // Comportamiento de partículas específico del enemigo (si lo hay)
    }
    public virtual void TriggerBehaviour(GameObject other)        //Comportamiento con TriggerEnter si es que tiene
    {
        // Comportamiento específico del trigger del enemigo (si lo hay)
    }

    private PowerUp GenerateRandomPowerUp()     
    {
        //IA2-LINQ
        //Creo un valor aleatorio entre 0 y 100 y tomo de la lista de power ups ordenados de menor probabilidad a mayor probabilidad
        //cual es el primero en el que el valor aleatorio sea menor a su probabilidad y lo devuelvo, si no no devuelvo nada
        float randomValue = UnityEngine.Random.Range(0f, 100f);
        return availablePowerUps.OrderBy(powerUp => powerUp.dropProbability).FirstOrDefault(powerUp => randomValue < powerUp.dropProbability)?.prefab;
    }
    #endregion
}
