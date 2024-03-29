using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{
    [SerializeField] private MissileStrategy missile;
    [SerializeField] private float life;
    private bool oneChance = true;
    private int id;
    [SerializeField] private bool isSpecial = false;
    //private int damage;
    private float  minStability, maxStability;
    [HideInInspector] public bool hasBeenAtracted = false;
    [HideInInspector] public float rotationSpeed = 100f;
    [HideInInspector] public float rotationDirection;
    private CircleCollider2D circleCollider2D;
    private Rigidbody2D rb2D;


    private void OnEnable() {
        id = missile.id;
        life = missile.maxLife;
        minStability = missile.minStability;
        maxStability = missile.maxStability;
        oneChance = true;
        circleCollider2D = GetComponent<CircleCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage){
        life -= damage;
        if(life<= 0 && oneChance){
            oneChance = false;
            life = 1;
        }
        if(life <= 0){
            missile.ExplosionBehaviour(transform);
            this.gameObject.SetActive(false);
        }
    }

    public void TryToShoot(Vector2 startPoint, Vector2 endPoint, int baseStability){
        float distance = Vector2.Distance(startPoint, endPoint);
        float probability = Random.Range(0,100);
        float maxProbability;
        if(distance <= 1){
            maxProbability = maxStability;
            maxProbability = maxProbability + (baseStability * 3.5f);
        }
        else{
            distance  = distance - 1;
            maxProbability = maxStability + (minStability - maxStability) * distance;
            maxProbability = Mathf.Clamp(maxProbability, minStability, maxStability);
            maxProbability = maxProbability + (baseStability * 3.5f);
        }
        if(probability > maxProbability){
            missile.ExplosionBehaviour(transform);
            this.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        float damage = missile.CollisionBehaviour(other.gameObject, this.gameObject);
        if(damage > 0){
            TakeDamage(damage);
        }
        //Debug.Log(rb2D.velocity.magnitude);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(circleCollider2D.isTrigger){
            float damage = missile.CollisionBehaviour(other.gameObject, this.gameObject);
            TakeDamage(damage);

        //Debug.Log(rb2D.velocity.magnitude);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(circleCollider2D.isTrigger){
            if(isSpecial){
                if (other.CompareTag("Enemy"))
                {
                    missile.SpecialBehaviourStay(other.gameObject, this.gameObject);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(circleCollider2D.isTrigger){
            if(isSpecial){
                if (other.CompareTag("Enemy"))
                {
                    missile.SpecialBehaviourExit(other.gameObject, this.gameObject);
                }
            }
        }
    }




}
