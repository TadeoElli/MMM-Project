using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    [SerializeField] private MissileStrategy missile;

    [SerializeField] private float life;
    private bool oneChance = true;
    private int damage;
    private float minDamage, maxDamage;


    private void OnEnable() {
        life = missile.maxLife;
        minDamage = missile.minDamage;
        maxDamage = missile.maxDamage;
        oneChance = true;
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
    }
    private void TakeDamage(){
        life -= damage;
        if(life<= 0 && oneChance){
            oneChance = false;
            life = 1;
        }
        if(life <= 0){
            CreateExplosion();
            this.gameObject.SetActive(false);
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
