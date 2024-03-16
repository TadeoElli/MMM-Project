using UnityEngine;

public abstract class ExplosionStrategy : ScriptableObject       
{
    public float radius;
    public ExplosionsTypes explosionType;

    public abstract void DealDamage(Transform origin);
    public abstract void ExplosionBehaviour(Transform origin);
    public abstract void ImplosionBehaviour(Transform origin);
    
}
