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
    public GameObject CreateMissile(Transform origin){
        GameObject missile = MissilePool.Instance.RequestMissile(prefab);
        missile.transform.position = origin.position;
        return  missile;
    } 
    public abstract void SpecialBehaviourStay(GameObject other,GameObject prefab); 
    public abstract void SpecialBehaviourExit(GameObject other,GameObject prefab); 
    public abstract int CollisionBehaviour(GameObject other, GameObject prefab);
    
    public void DealDamage(GameObject other, GameObject prefab){
        float damage = Random.Range(minDamage,maxDamage);
        if (other.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemy))
        {
            if(enemy.absorb){
                damage = damage * -1;
                prefab.SetActive(false);
            }
            enemy.TakeDamage(damage);
        }
    }
    
}

