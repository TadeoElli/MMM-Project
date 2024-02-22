using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : DamageTypes
{
    [SerializeField] private MissileStrategy missile;

    [SerializeField] private float life;
    private float minDamage, maxDamage;


    private void OnEnable() {
        life = missile.maxLife;
        minDamage = missile.minDamage;
        maxDamage = missile.maxDamage;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == 7){
            int damage = damageDictionary["Walls"];
            life -= damage;
            if(life <= 0){
                this.gameObject.SetActive(false);
            }
        }
        else if(other.gameObject.layer == 8){
            int damage = damageDictionary["SmallEnemies"];
            life -= damage;
            if(life <= 0){
                this.gameObject.SetActive(false);
            }
        }
    }
}
