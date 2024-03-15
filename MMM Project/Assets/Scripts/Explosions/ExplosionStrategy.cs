using UnityEngine;

public abstract class ExplosionStrategy : ScriptableObject       
{
    public int id;
    public float radius;
    public float damage;
    public GameObject prefab;
    public ExplosionsTypes explosionType;
    public GameObject CreateMissile(Transform origin){
        GameObject explosion = ExplosionPool.Instance.RequestExplosion(prefab);
        explosion.transform.position = origin.position;
        return  explosion;
    } 
    public abstract void DealDamage(Transform origin);
    public abstract void SpecialBehaviour(Transform origin);
    
}
