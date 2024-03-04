using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    [SerializeField] private MissileStrategy missile;
    [SerializeField] private float life;
    private bool oneChance = true;
    private int id;
    [SerializeField] private bool isSpecial = false;
    //private int damage;
    private float  minStability, maxStability;
    public bool hasBeenAtracted = false;
    public float rotationSpeed = 100f;
    public float rotationDirection;
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

    private void CreateExplosion(){
        GameObject explosion = ExplosionPool.Instance.RequestExplosion(missile.explosion);
        explosion.GetComponent<Explosion>().creatorId =  id;
        explosion.transform.position = transform.position;
        this.gameObject.SetActive(false);
    }
    public void TakeDamage(float damage){
        life -= damage;
        if(life<= 0 && oneChance){
            oneChance = false;
            life = 1;
        }
        if(life <= 0){
            CreateExplosion();
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
            CreateExplosion();
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        float damage = missile.CollisionBehaviour(other.gameObject, this.gameObject);
        if(damage > 0){
            TakeDamage(damage);
        }
        if(other.gameObject.layer != 7){
            if(isSpecial){
                missile.SpecialBehaviourEnter(other.gameObject, this.gameObject);
            }
        }
        //Debug.Log(rb2D.velocity.magnitude);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(circleCollider2D.isTrigger){
            float damage = missile.CollisionBehaviour(other.gameObject, this.gameObject);
            TakeDamage(damage);
/*
            if(other.gameObject.layer == 7){
                //missile.SpecialBehaviour(this.gameObject);
            }
            if(other.CompareTag("Enemy") && isSpecial){
                missile.SpecialBehaviourEnter(other.gameObject,this.gameObject);
            }*/
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
