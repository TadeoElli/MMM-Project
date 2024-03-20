using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemies/Repulse", order = 1)]
public class RepulseEnemyStrategy : EnemyStrategy
{
    [SerializeField] private float cooldown;
    [SerializeField] private GameObject particle;
    private float timer = 0;
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
    public override void ParticleBehaviour(GameObject specialParticle){
        if(timer > cooldown){
            timer = 0;
            specialParticle.SetActive(true);
        }
        else{
            timer = timer + 1 * Time.deltaTime;
        }
    }
    public override void TriggerBehaviour(GameObject other){

    }
    
}
