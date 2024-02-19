using UnityEngine;


public abstract class MissileStrategy : ScriptableObject
{
    public int id;
    public int energyConsumption;
    public int minDamage;
    public int maxDamage;
    public float velocity;
    public float minStability;
    public float maxStability;
    public GameObject prefab;
    public abstract void CreateMissile(); 
    //public abstract void Drag();

    //public abstract void Shoot();
    
}

