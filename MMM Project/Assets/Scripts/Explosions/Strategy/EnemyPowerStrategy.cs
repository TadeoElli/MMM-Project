using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Explosion", menuName = "ScriptableObject/Explosion/Enemy", order = 2)]
public class EnemyPowerStrategy : ExplosionStrategy
{
    [SerializeField] private float explosionForce;
    public override void DealDamage(Transform origin){

    }
    public override void ExplosionBehaviour(Transform origin){
        Collider2D[] objetos = Physics2D.OverlapCircleAll(origin.position, radius);

        foreach (Collider2D collisions in objetos){
            if(collisions.CompareTag("Missiles")){
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

    public override void ImplosionBehaviour(Transform origin){

    }

}
