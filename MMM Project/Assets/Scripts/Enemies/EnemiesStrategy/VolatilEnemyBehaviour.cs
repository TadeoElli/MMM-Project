using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemies/Volatil", order = 3)]
public class VolatilEnemyBehaviour : EnemyStrategy
{
    [SerializeField] GameObject nuclearExplosion;
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
                CollisionAction(other, prefab);
                return damage;
            case 9:
                damage = DamageTypes.Instance.collisionEnemiesDictionary[layer];
                CollisionAction(other, prefab);
                return damage;
            case 10:
                damage = DamageTypes.Instance.collisionEnemiesDictionary[layer];
                CollisionAction(other, prefab);
                return damage;
            case 11:
                damage = DamageTypes.Instance.collisionEnemiesDictionary[layer];
                return damage;
            default:
                damage = 0;
                return damage;
        }
    }

    private void CollisionAction(GameObject other, EnemyBehaviour prefab){
        if(other.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemy)){
            if(enemy.enemy == prefab.enemy){
                if(enemy.normalDir != prefab.normalDir){
                    CreateExplosion(other);
                }
            }
        }
        
    }

    private void CreateExplosion(GameObject other){
        GameObject explosion = ExplosionPool.Instance.RequestExplosion(nuclearExplosion);
        explosion.transform.position = other.transform.position;
    }

    public override GameObject DeathBehaviour(){
        GameObject explosion = ExplosionPool.Instance.RequestExplosion(base.explosion);
        return explosion;
    }

    public override void SpecialBehaviour(Transform origin){

    }
    
}

