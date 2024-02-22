using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    [SerializeField] private MissileStrategy missile;

    [SerializeField] private float life, distance, maxProbability;
    private bool oneChance = true;
    [SerializeField] private Vector2 originPosition;
    private int damage;
    private float minDamage, maxDamage, minStability, maxStability;


    private void OnEnable() {
        life = missile.maxLife;
        minDamage = missile.minDamage;
        maxDamage = missile.maxDamage;
        minStability = missile.minStability;
        maxStability = missile.maxStability;
        oneChance = true;
        originPosition = this.transform.position;
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CreateExplosion(){
        GameObject explosion = ExplosionPool.Instance.RequestExplosion();
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
        switch (other.gameObject.layer)
        {
            case 7:
                damage = DamageTypes.Instance.damageDictionary["Walls"];
                TakeDamage();
                break;
            case 8:
                damage = DamageTypes.Instance.damageDictionary["SmallEnemies"];
                TakeDamage();
                break;
            case 9:
                damage = DamageTypes.Instance.damageDictionary["MediumEnemies"];
                TakeDamage();
                break;
            case 10:
                damage = DamageTypes.Instance.damageDictionary["BigEnemies"];
                TakeDamage();
                break;
            case 11:
                damage = DamageTypes.Instance.damageDictionary["Bosses"];
                TakeDamage();
                break;
            default:
                break;
        }
    }
}
