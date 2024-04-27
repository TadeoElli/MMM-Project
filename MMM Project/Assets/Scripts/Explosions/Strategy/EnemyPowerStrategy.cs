using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Explosion", menuName = "ScriptableObject/Explosion/Enemy", order = 2)]
public class EnemyPowerStrategy : ExplosionStrategy
///Este tipo de explosiones no afectan a los enemigos, solo a los misiles
{
    [SerializeField] private float explosionForce;  //Fuerza de explosion
    public override void DealDamage(Transform origin){

    }
    public override void ExplosionBehaviour(Transform origin){  //Por cada collider dentro del rango, si es un misil lo empuja
        var objects = Physics2D.OverlapCircleAll(origin.position, radius)
            .OfType<Rigidbody2D>().Where(collision => collision.CompareTag("Missiles"))
            .ToList();
        foreach (var rb2D in objects){
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
