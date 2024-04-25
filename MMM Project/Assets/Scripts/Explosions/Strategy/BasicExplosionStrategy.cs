using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[CreateAssetMenu(fileName = "New Explosion", menuName = "ScriptableObject/Explosion/Basic", order = 0)]
public class BasicExplosionStrategy : ExplosionStrategy
///Este tipo de explosiones afecta a los enemigos como al nexo
{
    [SerializeField] private float implosionForce, explosionForce;
    public override void DealDamage(Transform origin){  //Por cada collider dentro del radio, si es un enemigo o el nexo le hace daño
        Collider2D[] objetos = Physics2D.OverlapCircleAll(origin.position, radius);

        foreach (Collider2D collisions in objetos){

            if(collisions.CompareTag("Nexus")){
                collisions.GetComponent<NexusCollisions>()?.TakeDamageForMissile(explosionType);
            }
            if(collisions.CompareTag("Enemy")){

                collisions.GetComponent<EnemyBehaviour>()?.TakeDamageForExplosion(explosionType);
            }
        }
    }
    public override void ExplosionBehaviour(Transform origin){   //Por cada collider dentro del radio, si es enemigo lo empuja
        Collider2D[] objects = Physics2D.OverlapCircleAll(origin.position, radius).Where(collision => collision.CompareTag("Enemy")).ToArray();
        foreach (var collision in objects)
        {
            Rigidbody2D rb2D = collision.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                Vector2 direction = collision.transform.position - origin.position;
                float distance = 1 + direction.magnitude;
                float finalForce = explosionForce / distance;
                rb2D.AddForce(direction * finalForce);
            }
        }
    }

    public override void ImplosionBehaviour(Transform origin){   //Por cada collider dentro del radio, si es enemigo lo atrae
        Collider2D[] objects = Physics2D.OverlapCircleAll(origin.position, radius).Where(collision => collision.CompareTag("Enemy")).ToArray();

        foreach (Collider2D collisions in objects){
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
