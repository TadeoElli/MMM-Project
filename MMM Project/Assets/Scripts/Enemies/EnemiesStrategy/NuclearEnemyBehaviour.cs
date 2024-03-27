using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemies/Nuclear", order = 2)]
public class NuclearEnemyBehaviour : EnemyStrategy
///Este tipo de enemigo tendra una cierta probabilidad de que al morir, en vez de crear una explosion de muerte
///Creee otra mas poderosa
{
    [SerializeField] private float probability;
    [SerializeField] private Explosion nuclearExplosion;        //La posible explosion
    public override int CollisionBehaviour(GameObject other, EnemyBehaviour prefab){       //Comportamiento de collisiones      
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
                CollisionForce(other, prefab);      //Llama a la funcion para empujar al otro enemigo
                return damage;
            default:
                damage = 0;
                return damage;
        }
    }

    public override GameObject DeathBehaviour(){    //Crea la explosion de muerte
        float number = Random.Range(0f,100f);      
        if(number < probability){   //si el numero random es menor a la probabilidad
            GameObject explosion = ExplosionPool.Instance.RequestExplosion(nuclearExplosion);   //Devuelve la explosion especial
            return explosion;
        }
        else{
            GameObject explosion = ExplosionPool.Instance.RequestExplosion(base.NewExplosion); //si no devuelve la explosion comun
            return explosion;
        }
    }

    public override void ParticleBehaviour(GameObject prefab){

    }
    public override void TriggerBehaviour(GameObject other){

    }
    
}

