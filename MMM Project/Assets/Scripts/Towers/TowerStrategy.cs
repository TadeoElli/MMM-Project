using UnityEngine;

public abstract class TowerStrategy : ScriptableObject        //Strategy para todos los tipos de missiles
{
    public int id;
    public float energyConsumption;
    public float maxEnergy;
    public float radius;
    public float cooldown;
    public GameObject prefab;
    public GameObject explosion;
    public abstract void CreateTower(Vector2 origin); 
    public abstract void SpecialBehaviour(GameObject prefab); 

    public abstract void ColliderBehaviour(GameObject prefab, GameObject other); 
    

    
}