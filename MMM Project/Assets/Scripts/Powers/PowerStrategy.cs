using UnityEngine;

public abstract class PowerStrategy : ScriptableObject        //Strategy para todos los tipos de missiles
{
    public int id;
    public float energyConsumption;
    public float cooldown;
    public bool hasPerformedCursor;

    public abstract bool BehaviourStarted();
    public abstract bool BehaviourPerformed();
    public abstract void BehaviourEnded();
    
}
