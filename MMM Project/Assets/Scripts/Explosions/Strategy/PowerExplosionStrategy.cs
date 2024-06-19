using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Explosion", menuName = "ScriptableObject/Explosion/Power", order = 1)]
public class PowerExplosionStrategy : ExplosionStrategy
///Este tipo de explosiones solo afectan a los enemigos
{//IA2-P2”.
    [SerializeField] private float implosionForce, explosionForce;
    public override void DealDamage(Transform origin){  //Por cada collider dentro del radio, si es enemigo llama a la funcion de hacer daño
    //y le manda el tipo de explosion
        var gridEntities = Query(origin).ToList();
        var gridEnemy = gridEntities.OfType<EnemyBehaviour>().Where(x => x.col.enabled == true).ToList();

        foreach (var collisions in gridEnemy){
            collisions.TakeDamageForExplosion(explosionType);
        }
    }
    public override void ExplosionBehaviour(Transform origin){  //Por cada collider dentro del radio, si es enemigo lo empuja
    //IA2-LINQ
    //IA2-P1"
    //Toma todos los objetos dentro de un radio y guardo solo los de tipo rigidbody que tengan el tag correspondiente
        var enemyBehaviours = Query(origin).OfType<EnemyBehaviour>().Where(x => x.col.enabled == true).ToList();

        // Usar Aggregate para reducir objects a una lista de Rigidbody2D
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

    public override void ImplosionBehaviour(Transform origin){  //Por cada collider dentro del radio, si es enemigo lo atrae
    //IA2-LINQ
    //IA2-P1"
    //Toma todos los objetos dentro de un radio y guardo solo los de tipo rigidbody que tengan el tag correspondiente
        var enemyBehaviours = Query(origin).OfType<EnemyBehaviour>().Where(x => x.col.enabled == true).ToList();
        // Usar Aggregate para reducir objects a una lista de Rigidbody2D
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
