using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject enemyPierced;
    [SerializeField] private MissileStrategy missile;
    [SerializeField] private MissileBehavior missileBehavior;
    [SerializeField] private float force, bounceCoef;
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
        else if(other.gameObject.layer == 7){
            Vector2 bounceDirection = (Vector2)transform.position - (Vector2)other.transform.position;
            bounceDirection = Vector2.Reflect(bounceDirection.normalized, Vector2.zero);

            GetComponent<Rigidbody2D>().velocity = bounceDirection * (missile.velocity / 3) * bounceCoef;
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
