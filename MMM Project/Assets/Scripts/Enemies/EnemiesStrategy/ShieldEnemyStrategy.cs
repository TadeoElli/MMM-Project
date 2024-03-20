using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemies/Shield", order = 4)]
public class ShieldEnemyStrategy : EnemyStrategy
{
    
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
        GameObject explosion = ExplosionPool.Instance.RequestExplosion(base.explosion);
        return explosion;
    }
    public override void ParticleBehaviour(GameObject other){
        
    }

    public override void TriggerBehaviour(GameObject other){
        if(other.CompareTag("Missiles")){
            Rigidbody2D rb2D = other.GetComponent<Rigidbody2D>();
            Vector2 bounceDirection = rb2D.velocity;
            bounceDirection.x = bounceDirection.x * -1;
            bounceDirection.y = bounceDirection.y * -1;

            rb2D.velocity = bounceDirection;
        }
    }
    
}

