using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemies/Volatil", order = 3)]
public class VolatilEnemyBehaviour : EnemyStrategy  
///Este enemigo solo se mueve en una direccion pero si colisiona con otro que sea del mismo tipo
///y viene en la direccion contraria entonces ambos crearan una explosion nuclear que destruira a todos los objetos del mapa
{
    [SerializeField] Explosion nuclearExplosion;        //Explosion que genera al chocar con otro enemigo de su mismo tipo
    public override int CollisionBehaviour(GameObject other, EnemyBehaviour prefab){        //Comportamiento de collisiones    
        int layer = other.layer;    
        int damage;
        switch (layer)      //Dependiendo del layer con el choco llama a un LookUpTable de tipos de daño de colisiones
        {
            case 7:  
                damage = DamageTypes.Instance.collisionEnemiesDictionary[layer];
                return damage;
            case 8:
            case 9:
            case 10:
                damage = DamageTypes.Instance.collisionEnemiesDictionary[layer];
                CollisionAction(other, prefab); // Llama al comportamiento de collision si choca con los respectivos layers
                CollisionForce(other, prefab);  //Llama a la funcion para empujar al otro enemigo
                return damage;
            default:
                damage = 0;
                return damage;
        }
    }

    private void CollisionAction(GameObject other, EnemyBehaviour prefab){
        if(other.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemy)){   //si el otro objeto tambien es volatil y su direccion es la opuesta
            if(enemy.Enemy == prefab.Enemy){
                if(enemy.normalDir != prefab.normalDir){
                    CreateExplosion(other);     //Crea la explosion nuclear
                }
            }
        }
        
    }

    private void CreateExplosion(GameObject other){     //Crea la explosion nuclear
        GameObject explosion = ExplosionPool.Instance.RequestExplosion(nuclearExplosion);
        explosion.transform.position = other.transform.position;
    }

    public override GameObject DeathBehaviour(){        //Crea la explosion de muerte
        GameObject explosion = ExplosionPool.Instance.RequestExplosion(base.NewExplosion);
        return explosion;
    }

    public override void ParticleBehaviour(GameObject prefab){

    }
    public override void TriggerBehaviour(GameObject other){

    }
    
}

