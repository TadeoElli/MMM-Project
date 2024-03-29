using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemies/Shield", order = 4)]
public class ShieldEnemyStrategy : EnemyStrategy
///Este enemigo tendra un escudo delante suyo que rebota los misiles entrantes, por lo que solo podra ser dañado por los costados
///o por detras
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
                CollisionForce(other, prefab);  //Llama a la funcion para empujar al otro enemigo
                return damage;
            default:
                damage = 0;
                return damage;
        }
    }

    public override GameObject DeathBehaviour(){    //Crea la explosion de muerte
        GameObject explosion = ExplosionPool.Instance.RequestExplosion(base.NewExplosion);
        return explosion;
    }
    public override void ParticleBehaviour(GameObject other){
        
    }

    public override void TriggerBehaviour(GameObject other){    //Si el trigger recibe un enter y es un misil, es porque choco con el trigger del escudo
        if(other.CompareTag("Missiles")){
            Rigidbody2D rb2D = other.GetComponent<Rigidbody2D>();       //Por lo que toma el rigidbody del misil y su direccion y lo devuelve en la direccion contraria
            Vector2 bounceDirection = rb2D.velocity;
            bounceDirection.x = bounceDirection.x * -1;
            bounceDirection.y = bounceDirection.y * -1;

            rb2D.velocity = bounceDirection;
        }
    }
    
}

