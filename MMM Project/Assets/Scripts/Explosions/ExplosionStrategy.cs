using UnityEngine;

public abstract class ExplosionStrategy : ScriptableObject       
{
    public float radius;      //Radio de explosion
    public ExplosionsTypes explosionType;     //Tipo de explosion, sirve para el LookUpTable de daños


    public abstract void DealDamage(Transform origin);      //Funcion que se encarga de hacer daño a las entidades
    public abstract void ExplosionBehaviour(Transform origin);      //Funcion de explosion
    public abstract void ImplosionBehaviour(Transform origin);      //Funcion de implosion
    
}
