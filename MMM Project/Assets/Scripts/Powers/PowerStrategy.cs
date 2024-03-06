using UnityEngine;

public abstract class PowerStrategy : ScriptableObject        //Strategy para todos los tipos de missiles
{
    public int id;
    public float energyConsumption;
    public float cooldown;

    public abstract void Behaviour(Vector3 position);
    
}
