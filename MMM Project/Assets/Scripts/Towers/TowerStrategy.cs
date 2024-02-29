using UnityEngine;

public abstract class TowerStrategy : ScriptableObject        //Strategy para todos los tipos de missiles
{
    public int id;
    public float energyConsumption;
    public float maxEnergy;
    public float cooldown;
    public GameObject prefab;
    public GameObject explosion;
    public abstract void CreateTower(Vector2 origin); 
    public abstract void SpecialBehaviour(GameObject prefab, GameObject other); 

    public abstract bool ColliderBehaviour(GameObject prefab, GameObject other); 

    public abstract void DestroyTower(GameObject prefab);
    

    
}