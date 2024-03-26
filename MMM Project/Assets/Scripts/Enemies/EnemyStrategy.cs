using UnityEngine;
using System;
using System.Collections.Generic;



public abstract class EnemyStrategy : ScriptableObject        //Strategy para todos los tipos de missiles
{
    [Serializable] 
    public class PowerUpData
    {
        [SerializeField]public PowerUp prefab;
        [SerializeField]public float dropProbability;
    }
    public float maxLife;
    public float rotationSpeed;
    public float collisionForce;
    public float velocity;
    public Explosion explosion;

    [Header("Will spawn the first prefab on the list")]
    [SerializeField] public List<PowerUpData> availablePowerUps;
    public abstract int CollisionBehaviour(GameObject other,EnemyBehaviour prefab);
    
    public void CollisionForce(GameObject other, EnemyBehaviour prefab){
        Rigidbody2D rb2D = other.GetComponent<Rigidbody2D>();

        Vector2 direction = other.transform.position - prefab.transform.position;
            
        rb2D.AddForce(direction.normalized * collisionForce, ForceMode2D.Force);
    }
    public abstract GameObject DeathBehaviour();
    public void Death(Transform origin) {
        GameObject powerUp = GenerateRandomPowerUp();
        if(powerUp != null){
            GameObject newPowerUp = PowerUpPool.Instance.RequestPowerUp(powerUp);
            newPowerUp.transform.position = origin.position;
        }
    }
    public abstract void ParticleBehaviour(GameObject particle);
    public abstract void TriggerBehaviour(GameObject other);

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
