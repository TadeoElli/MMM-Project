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
    //IA2-LINQ
    //Toma todos los objetos dentro de un radio y guardo solo los de tipo rigidbody que tengan el tag correspondiente
        var objects = Physics2D.OverlapCircleAll(origin.position, radius)
            .OfType<Rigidbody2D>().Where(collision => collision.CompareTag("Enemy"))
            .ToList();

        foreach (var rb2D in objects)
        {
            if (rb2D != null)
            {
                Vector2 direction = rb2D.transform.position - origin.position;
                float distance = 1 + direction.magnitude;
                float finalForce = explosionForce / distance;
                rb2D.AddForce(direction * finalForce);
            }
        }
    }

    public override void ImplosionBehaviour(Transform origin){   //Por cada collider dentro del radio, si es enemigo lo atrae
    //IA2-LINQ
    //Toma todos los objetos dentro de un radio y guardo solo los de tipo rigidbody que tengan el tag correspondiente
        var objects = Physics2D.OverlapCircleAll(origin.position, radius)
            .OfType<Rigidbody2D>().Where(collision => collision.CompareTag("Enemy"))
            .ToList();
        foreach (var rb2D in objects){
            if(rb2D != null){
                Vector2 direction = rb2D.transform.position - origin.position;
                float distance = 1 + direction.magnitude;
                float finalForce = implosionForce / distance;
                rb2D.AddForce(-direction * finalForce);
            }
        }
    }

}
