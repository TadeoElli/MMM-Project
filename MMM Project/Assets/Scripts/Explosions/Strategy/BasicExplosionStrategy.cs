using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[CreateAssetMenu(fileName = "New Explosion", menuName = "ScriptableObject/Explosion/Basic", order = 0)]
public class BasicExplosionStrategy : ExplosionStrategy
///Este tipo de explosiones afecta a los enemigos como al nexo
{
    //IA2-P2”.
    [SerializeField] private float implosionForce, explosionForce;
    public override void DealDamage(Transform origin){  //Por cada collider dentro del radio, si es un enemigo o el nexo le hace daño
        //Collider2D[] objetos = Physics2D.OverlapCircleAll(origin.position, radius);
        //Debug.Log($"Dealing damage from origin {origin.position}");
        var gridEntities = Query(origin).ToList();
        var gridEnemy = gridEntities.OfType<EnemyBehaviour>().Where(x => x.col.enabled == true).ToList();
        var gridNexus = gridEntities.OfType<NexusCollisions>().FirstOrDefault();
        //Debug.Log($"Found {gridEnemy.Count()} enemies.");
        foreach (var collisions in gridEnemy){
            collisions.TakeDamageForExplosion(explosionType);
        }
        /*foreach (var collisions in gridNexus){
            collisions.TakeDamageForMissile(explosionType);
        }*/
        gridNexus?.TakeDamageForMissile(explosionType);
    }
    public override void ExplosionBehaviour(Transform origin){   //Por cada collider dentro del radio, si es enemigo lo empuja
    //IA2-LINQ
    //IA2-P1"
    //Toma todos los objetos dentro de un radio y guardo solo los de tipo rigidbody que tengan el tag correspondiente
        var enemyBehaviours = Query(origin).OfType<EnemyBehaviour>().Where(x => x.col.enabled == true).ToList();
        // Usar Aggregate para reducir enemyBehaviours a una lista de Rigidbody2D
        var rigidbodies = enemyBehaviours.Aggregate(new List<Rigidbody2D>(), (list, enemy) =>
            {
                var rb = enemy.gameObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 direction = rb.transform.position - origin.position;
                    float distance = 1 + direction.magnitude;
                    float finalForce = explosionForce / distance;
                    rb.AddForce(direction * finalForce);
                }
                return list;
            });


    }

    public override void ImplosionBehaviour(Transform origin){   //Por cada collider dentro del radio, si es enemigo lo atrae
    //IA2-LINQ
    //IA2-P1"
    //Toma todos los objetos dentro de un radio y guardo solo los de tipo rigidbody que tengan el tag correspondiente
        var enemyBehaviours = Query(origin).OfType<EnemyBehaviour>().Where(x => x.col.enabled == true).ToList();
        // Usar Aggregate para reducir enemyBehaviours a una lista de Rigidbody2D
        var rigidbodies = enemyBehaviours.Aggregate(new List<Rigidbody2D>(), (list, enemy) =>
            {
                var rb = enemy.gameObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 direction = rb.transform.position - origin.position;
                    float distance = 1 + direction.magnitude;
                    float finalForce = implosionForce / distance;
                    rb.AddForce(-direction * finalForce);
                }
                return list;
            });
       
    }

    



}
