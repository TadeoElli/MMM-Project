using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemies/Basic", order = 0)]
public class BasicEnemyStrategy : EnemyStrategy
///Este tipo de enemigo no tiene ningun comportamiento especial, solo se mueve hacia adelante
{
    
    public override int CollisionBehaviour(GameObject other, EnemyBehaviour prefab){      //Comportamiento de collisiones       
        int layer = other.layer;
        int damage;
        switch (layer)  //Dependiendo del layer con el choco llama a un LookUpTable de tipos de daño de colisiones
        {
            case 7:
                damage = DamageTypes.Instance.collisionEnemiesDictionary[layer];
                return damage;
            case 8:
            case 9:
            case 10:
                damage = DamageTypes.Instance.collisionEnemiesDictionary[layer];
                CollisionForce(other, prefab);   //Llama a la funcion para empujar al otro enemigo
                return damage;
            default:
                damage = 0;
                return damage;
        }
    }

    public override GameObject DeathBehaviour(){      //Crea la explosion de muerte
        GameObject explosion = ExplosionPool.Instance.RequestExplosion(base.NewExplosion);
        return explosion;
    }
    public override void ParticleBehaviour(GameObject prefab){

    }
    public override void TriggerBehaviour(GameObject other){

    }
    
}

