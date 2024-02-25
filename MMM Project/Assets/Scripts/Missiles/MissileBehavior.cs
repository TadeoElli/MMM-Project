using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    [SerializeField] private MissileStrategy missile;
    [SerializeField] private float life;
    private bool oneChance = true;
    [SerializeField] private bool isSpecial = false;
    //private int damage;
    private float  minStability, maxStability;


    private void OnEnable() {
        life = missile.maxLife;
        minStability = missile.minStability;
        maxStability = missile.maxStability;
        oneChance = true;
    }

    private void CreateExplosion(){
        GameObject explosion = ExplosionPool.Instance.RequestExplosion();
        explosion.GetComponent<Explosion>().creator =  missile;
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

    public void TryToShoot(Vector2 startPoint, Vector2 endPoint){
        float distance = Vector2.Distance(startPoint, endPoint);
        float probability = Random.Range(0,100);
        float maxProbability;
        if(distance <= 1){
            maxProbability = maxStability;
        }
        else{
            distance  = distance - 1;
            maxProbability = maxStability + (minStability - maxStability) * distance;
            maxProbability = Mathf.Clamp(maxProbability, minStability, maxStability);
            
        }
        if(probability > maxProbability){
            CreateExplosion();
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        float damage = missile.CollisionBehaviour(other.gameObject.layer);
        if(damage > 0){
            TakeDamage(damage);
        }
        if(other.gameObject.layer != 7){
            if(isSpecial){
                missile.SpecialBehaviour(this.gameObject);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        CircleCollider2D circleCollider2D = GetComponent<CircleCollider2D>();
        if(circleCollider2D.isTrigger){
            if(other.gameObject.layer == 7){
                missile.SpecialBehaviour(this.gameObject);
            }
        }
    }


}
