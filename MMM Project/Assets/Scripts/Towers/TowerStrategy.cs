using UnityEngine;

public abstract class TowerStrategy : ScriptableObject        //Strategy para todos los tipos de missiles
{
    public int id;
    public float energyConsumption;
    public float maxEnergy;
    public float cooldown;
    public TowerBehaviour prefab;
    public Explosion explosion;
    public void CreateTower(Vector2 origin){
        GameObject tower = TowersPool.Instance.RequestTower(prefab);
        tower.transform.position = origin;
    }
    public abstract void SpecialBehaviour(GameObject prefab, GameObject other); 

    public abstract bool ColliderBehaviour(GameObject prefab, GameObject other); 

    public abstract void DestroyTower(GameObject prefab);
    

    
}