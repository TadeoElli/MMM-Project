using UnityEngine;
using System.Collections.Generic;

public abstract class ExplosionStrategy : ScriptableObject      , IQuery 
{
    public float radius;      //Radio de explosion
    public ExplosionsTypes explosionType;     //Tipo de explosion, sirve para el LookUpTable de daños

    public abstract void DealDamage(Transform origin);      //Funcion que se encarga de hacer daño a las entidades
    public abstract void ExplosionBehaviour(Transform origin);      //Funcion de explosion
    public abstract void ImplosionBehaviour(Transform origin);      //Funcion de implosion
    
    public IEnumerable<IGridEntity> Query(Transform origin)
    {
        float r = radius /2;
        return SpatialGrid.Instance.Query(
                                origin.position + new Vector3(-r, 0, -r),
                                origin.position + new Vector3(r, 0, r),
                                x => Vector3.Distance(x, origin.position) < radius);
    }
}
