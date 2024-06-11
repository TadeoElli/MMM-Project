using UnityEngine;
using System.Collections.Generic;

public abstract class ExplosionStrategy : ScriptableObject, IQuery 
{
    public float radius;      //Radio de explosion
    public ExplosionsTypes explosionType;     //Tipo de explosion, sirve para el LookUpTable de daños

    public abstract void DealDamage(Transform origin);      //Funcion que se encarga de hacer daño a las entidades
    public abstract void ExplosionBehaviour(Transform origin);      //Funcion de explosion
    public abstract void ImplosionBehaviour(Transform origin);      //Funcion de implosion
    //IA2-P2”.
    public IEnumerable<IGridEntity> Query(Transform origin)
    {
        float r = radius;
        var entities = SpatialGrid.Instance.Query(
            origin.position + new Vector3(-r, -r, 0),
            origin.position + new Vector3(r, r, 0),
            x => Vector3.Distance(x, origin.position) < radius);
        
        //Debug.Log($"Query from {origin.position - new Vector3(r, r, 0)} to {origin.position + new Vector3(r, r, 0)} entities.");

        return entities;
    }
}
