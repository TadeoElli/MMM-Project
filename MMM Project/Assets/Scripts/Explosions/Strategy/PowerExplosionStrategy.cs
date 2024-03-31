using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Explosion", menuName = "ScriptableObject/Explosion/Power", order = 1)]
public class PowerExplosionStrategy : ExplosionStrategy
///Este tipo de explosiones solo afectan a los enemigos
{
    [SerializeField] private float implosionForce, explosionForce;
    public override void DealDamage(Transform origin){  //Por cada collider dentro del radio, si es enemigo llama a la funcion de hacer daño
    //y le manda el tipo de explosion
        Collider2D[] objetos = Physics2D.OverlapCircleAll(origin.position, radius);

        foreach (Collider2D collisions in objetos){

            if(collisions.CompareTag("Enemy")){

                collisions.GetComponent<EnemyBehaviour>().TakeDamageForExplosion(explosionType);
            }
        }
    }
    public override void ExplosionBehaviour(Transform origin){  //Por cada collider dentro del radio, si es enemigo lo empuja
        Collider2D[] objetos = Physics2D.OverlapCircleAll(origin.position, radius);

        foreach (Collider2D collisions in objetos){
            if(collisions.CompareTag("Enemy")){
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

    public override void ImplosionBehaviour(Transform origin){  //Por cada collider dentro del radio, si es enemigo lo atrae
        Collider2D[] objetos = Physics2D.OverlapCircleAll(origin.position, radius);

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
