using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Explosion", menuName = "ScriptableObject/Explosion/Shockwave", order = 2)]
public class ShockwaveExplosionStrategy : ExplosionStrategy
{
    [SerializeField] private float force;
    public override void DealDamage(Transform origin){

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
                    float finalForce = force / distance;
                    rb2D.AddForce(direction * finalForce);
                }
            }
        }
    }

}
