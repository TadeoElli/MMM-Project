using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float radius, force;
    [SerializeField] public AnimEvents events;
    [SerializeField] public MissileStrategy creator;

    private void Start() {
        events.ADD_EVENT("dealDamage", DealDamage);
        events.ADD_EVENT("end", DisableObject);
    }

    private void DealDamage(){
        Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D collisions in objetos){

            if(collisions.gameObject.tag == "Nexus"){
                collisions.GetComponent<NexusCollisions>().TakeDamage(creator);
            }
            if(collisions.gameObject.layer != 2){
                Rigidbody2D rb2D = collisions.GetComponent<Rigidbody2D>();
                if(rb2D != null){
                    Vector2 direction = collisions.transform.position - transform.position;
                    float distance = 1 + direction.magnitude;
                    float finalForce = force / distance;
                    rb2D.AddForce(direction * finalForce);
                }
            }
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void DisableObject(){
        this.gameObject.SetActive(false);
    }
}
