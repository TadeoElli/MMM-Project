using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Explosion", menuName = "ScriptableObject/Explosion/Implosion", order = 2)]
public class ImplosionExplosionStrategy : ExplosionStrategy
{
    [SerializeField] private float implosionForce, explosionForce;
    public override void DealDamage(Transform origin){
        Collider2D[] objetos = Physics2D.OverlapCircleAll(origin.position, radius);

        foreach (Collider2D collisions in objetos){

            if(collisions.CompareTag("Enemy")){

                collisions.GetComponent<EnemyBehaviour>().TakeDamageForExplosion(explosionType);
                Rigidbody2D rb2D = collisions.GetComponent<Rigidbody2D>();
                if(rb2D != null){
                    Vector2 direction = collisions.transform.position - origin.position;
                    float distance = 1 + direction.magnitude;
                    float finalForce = explosionForce / distance;
                    rb2D.AddForce(direction * finalForce);
                }
            }
        }
    }
    public override void SpecialBehaviour(Transform origin){
        Collider2D[] objetos = Physics2D.OverlapCircleAll(origin.position, radius);
        Debug.Log("Shockwave");

        foreach (Collider2D collisions in objetos){
            if(collisions.CompareTag("Enemy")){
                Rigidbody2D rb2D = collisions.GetComponent<Rigidbody2D>();
                if(rb2D != null){
                    Vector2 direction = collisions.transform.position - origin.position;
                    float distance = 1 + direction.magnitude;
                    float finalForce = implosionForce / distance;
                    rb2D.AddForce(-direction * finalForce);
                }
            }
        }
    }

}
