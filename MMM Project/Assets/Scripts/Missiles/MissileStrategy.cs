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
    public abstract GameObject CreateMissile(Transform origin); 
    public abstract void SpecialBehaviour(Rigidbody2D rigidbody2D); 
    public abstract int CollisionBehaviour(int layer);
    

    
}

