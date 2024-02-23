using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    [SerializeField] private MissileStrategy missile;

    [SerializeField] private float life, distance, maxProbability;
    private bool oneChance = true;
    [SerializeField] private bool isSpecial = false;
    [SerializeField] private Vector2 originPosition;
    private int damage;
    public float energyConsumption;
    private float minDamage, maxDamage, minStability, maxStability;


    private void OnEnable() {
        life = missile.maxLife;
        energyConsumption = missile.energyConsumption;
        minDamage = missile.minDamage;
        maxDamage = missile.maxDamage;
        minStability = missile.minStability;
        maxStability = missile.maxStability;
        oneChance = true;
        originPosition = this.transform.position;
    }
    private void Update() {
        this.gameObject.GetComponent<Rigidbody2D>().rotation += 30f;
    }

    private void CreateExplosion(){
        GameObject explosion = ExplosionPool.Instance.RequestExplosion();
        explosion.GetComponent<Explosion>().creator =  missile;
        explosion.transform.position = transform.position;
        this.gameObject.SetActive(false);
    }
    private void TakeDamage(){
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
        distance = Vector2.Distance(startPoint, endPoint);
        float probability = Random.Range(0,100);
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
        if(isSpecial){
            missile.SpecialBehaviour(this.gameObject.GetComponent<Rigidbody2D>());
        }
        switch (other.gameObject.layer)
        {
            case 7:
                damage = DamageTypesForMissiles.Instance.damageDictionary["Walls"];
                TakeDamage();
                break;
            case 8:
                damage = DamageTypesForMissiles.Instance.damageDictionary["SmallEnemies"];
                TakeDamage();
                break;
            case 9:
                damage = DamageTypesForMissiles.Instance.damageDictionary["MediumEnemies"];
                TakeDamage();
                break;
            case 10:
                damage = DamageTypesForMissiles.Instance.damageDictionary["BigEnemies"];
                TakeDamage();
                break;
            case 11:
                damage = DamageTypesForMissiles.Instance.damageDictionary["Bosses"];
                TakeDamage();
                break;
            default:
                break;
        }
    }
}
