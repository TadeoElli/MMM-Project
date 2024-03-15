using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Explosion", menuName = "ScriptableObject/Explosion/Basic", order = 0)]
public class BasicExplosionStrategy : ExplosionStrategy
{
    public override void DealDamage(Transform origin){
        Collider2D[] objetos = Physics2D.OverlapCircleAll(origin.position, radius);

        foreach (Collider2D collisions in objetos){

            if(collisions.CompareTag("Nexus")){
                collisions.GetComponent<NexusCollisions>().TakeDamageForMissile(explosionType);
            }
        }
    }
    public override void SpecialBehaviour(Transform origin){

    }

}
