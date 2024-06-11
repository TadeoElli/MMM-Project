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
    //Toma todos los objetos dentro de un radio y guardo solo los de tipo rigidbody que tengan el tag correspondiente
        var objects = Query(origin).OfType<MissileBehaviour>().ToList();
        List<Rigidbody2D> rigidbodies = new List<Rigidbody2D>();

        foreach (var missile in objects)
        {
            var rb = missile.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rigidbodies.Add(rb);
            }
        }

        foreach (var rb2D in rigidbodies){
            if(rb2D != null){
                Vector2 direction = rb2D.transform.position - origin.position;
                float distance = 1 + direction.magnitude;
                float finalForce = explosionForce / distance;
                rb2D.AddForce(direction * finalForce);
            }
        }
    }

    public override void ImplosionBehaviour(Transform origin){

    }

}
