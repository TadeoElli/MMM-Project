using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject enemyPierced;
    [SerializeField] private MissileStrategy missile;
    [SerializeField] private MissileBehavior missileBehavior;
    [SerializeField] private float force;
    private int damage;
    private Vector2 direction;

    private void OnTriggerEnter2D(Collider2D other) {
        damage = missile.CollisionBehaviour(other.gameObject.layer);
        missileBehavior.TakeDamage(damage);
        if (other.gameObject.tag == "Enemy")
        {
            enemyPierced = other.gameObject;
            Rigidbody2D rb2D = enemyPierced.GetComponent<Rigidbody2D>();
                if(rb2D != null){
                    Vector2 direction = enemyPierced.transform.position - transform.position;
                    float distance = 1 + direction.magnitude;
                    float finalForce = force / distance;
                    rb2D.AddForce(direction * finalForce);
                }
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject == enemyPierced)
        {
            direction = ((Vector2)enemyPierced.transform.position - (Vector2)transform.position).normalized;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject == enemyPierced)
        {
            enemyPierced = null;
            GetComponent<Rigidbody2D>().velocity = direction * 5;
            
        }
    }

}
