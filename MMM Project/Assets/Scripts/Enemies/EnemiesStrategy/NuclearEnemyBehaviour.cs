using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemies/Nuclear", order = 2)]
public class NuclearEnemyBehaviour : EnemyStrategy
{
    [SerializeField] private float probability;
    [SerializeField] private GameObject nuclearExplosion;
    public override int CollisionBehaviour(GameObject other, EnemyBehaviour prefab){            
        int layer = other.layer;
        int damage;
        switch (layer)
        {
            case 7:
                damage = DamageTypes.Instance.collisionEnemiesDictionary[layer];
                return damage;
            case 8:
                damage = DamageTypes.Instance.collisionEnemiesDictionary[layer];
                CollisionForce(other, prefab);
                return damage;
            case 9:
                damage = DamageTypes.Instance.collisionEnemiesDictionary[layer];
                CollisionForce(other, prefab);
                return damage;
            case 10:
                damage = DamageTypes.Instance.collisionEnemiesDictionary[layer];
                CollisionForce(other, prefab);
                return damage;
            case 11:
                damage = DamageTypes.Instance.collisionEnemiesDictionary[layer];
                CollisionForce(other, prefab);
                return damage;
            default:
                damage = 0;
                return damage;
        }
    }

    public override GameObject DeathBehaviour(){
        float number = Random.Range(0f,100f);
        if(number < probability){
            GameObject explosion = ExplosionPool.Instance.RequestExplosion(nuclearExplosion);
            return explosion;
        }
        else{
            GameObject explosion = ExplosionPool.Instance.RequestExplosion(base.explosion);
            return explosion;
        }
    }
    
}

