using UnityEngine;


public abstract class MissileStrategy : ScriptableObject        //Strategy para todos los tipos de missiles
{
    public int id;
    public float energyConsumption;
    public float maxLife;
    public float minDamage;
    public float maxDamage;
    public float velocity;
    public float minStability;
    public float maxStability;
    public GameObject prefab;
    public GameObject explosion;
    public abstract GameObject CreateMissile(Transform origin); 
    public abstract void SpecialBehaviourEnter(GameObject other,GameObject prefab); 
    public abstract void SpecialBehaviourStay(GameObject other,GameObject prefab); 
    public abstract void SpecialBehaviourExit(GameObject other,GameObject prefab); 
    public abstract int CollisionBehaviour(GameObject other, GameObject prefab);
    
    public void DealDamage(GameObject other){
        float damage = Random.Range(minDamage,maxDamage);
        if (other.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemy))
        {
            enemy.TakeDamage(damage);
        }
    }
    
}

