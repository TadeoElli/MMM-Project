using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Missile", menuName = "ScriptableObject/Missiles/Basic", order = 0)]
public class BasicMissilesStrategy : MissileStrategy
{

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
    public override void ExplosionBehaviour(Transform origin){
        GameObject newExplosion = ExplosionPool.Instance.RequestExplosion(base.explosion);
        newExplosion.transform.position = origin.position;
    }
        
    


}
