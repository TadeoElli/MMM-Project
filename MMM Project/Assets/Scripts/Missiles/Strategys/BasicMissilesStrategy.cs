using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Missile", menuName = "ScriptableObject/Missiles/Basic", order = 0)]
public class BasicMissilesStrategy : MissileStrategy
///Este tipo de misiles no tienen ningun funcionamiento especial, sin embargo pueden variar en daño, velocidad
///el tipo de material para manejar los rebotes o la masa
{
    //El comportamiento de colisiones
    public override int CollisionBehaviour(GameObject other, GameObject prefab){
        int layer = other.layer;
        int damage;
        switch (layer)
        {
            case 7:
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                return damage;
            case 8:
            case 9:
            case 10:
            case 11:
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                DealDamage(other, prefab);
                return damage;
            default:
                damage = 0;
                return damage;
        }
    }

    public override void SpecialBehaviourStay(GameObject other,GameObject prefab){

    }
    public override void SpecialBehaviourExit(GameObject other,GameObject prefab){

    }
        //Crea la explosion correspondiente al quedarse sin vida
    public override void ExplosionBehaviour(Transform origin){
        GameObject newExplosion = ExplosionPool.Instance.RequestExplosion(base.NewExplosion);
        newExplosion.transform.position = origin.position;
    }
        
    


}
