using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Missile", menuName = "ScriptableObject/Missiles/Antimatter", order = 4)]
public class AntimatterMissileBehaviour : MissileStrategy
//Este tipo de misil se activara con un power up y lo que hara sera que cuando choque con un enemigo explote
{

    //El comportamiento de cuando colisiona
    public override int CollisionBehaviour(GameObject other, GameObject prefab){
        MissileBehaviour missileBehaviour = prefab.GetComponent<MissileBehaviour>();
            
        int layer = other.layer;
        int damage;
        switch (layer)
        {
            case 7:
                damage = 0;
                Rigidbody2D rb2D = prefab.GetComponent<Rigidbody2D>();
                Vector2 bounceDirection = rb2D.velocity;
                bounceDirection.y = bounceDirection.y * -1;

                rb2D.velocity = bounceDirection;
                return damage;
            case 8:
            case 9:
            case 10:
            case 11:    //Recibe 2 veces daño por el "oneChance" que hace que despues de que se quede  con 0 de vida le 
                //permite aguantar un rebote mas
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                missileBehaviour.TakeDamage(10);
                missileBehaviour.TakeDamage(10);
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
