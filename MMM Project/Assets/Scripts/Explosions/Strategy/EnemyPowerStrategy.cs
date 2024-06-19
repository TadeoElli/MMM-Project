using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Explosion", menuName = "ScriptableObject/Explosion/Enemy", order = 2)]
public class EnemyPowerStrategy : ExplosionStrategy
///Este tipo de explosiones no afectan a los enemigos, solo a los misiles
{//IA2-P2”.
    [SerializeField] private float explosionForce;  //Fuerza de explosion
    public override void DealDamage(Transform origin){

    }
    public override void ExplosionBehaviour(Transform origin){  //Por cada collider dentro del rango, si es un misil lo empuja
    //IA2-LINQ
    //IA2-P1"
    //Toma todos los objetos dentro de un radio y guardo solo los de tipo rigidbody que tengan el tag correspondiente
        var objects = Query(origin).OfType<MissileBehaviour>().ToList();
        // Usar Aggregate para reducir objects a una lista de Rigidbody2D
        var rigidbodies = objects.Aggregate(new List<Rigidbody2D>(), (list, missile) =>
            {
                var rb = missile.gameObject.GetComponent<Rigidbody2D>();
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

    public override void ImplosionBehaviour(Transform origin){

    }

}
