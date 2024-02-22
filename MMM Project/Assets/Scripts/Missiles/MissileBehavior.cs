using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour
{
    [SerializeField] private MissileStrategy missile;

    [SerializeField] private float life;
    [SerializeField] private int damage;
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
        switch (other.gameObject.layer)
        {
            case 7:
                damage = DamageTypes.Instance.damageDictionary["Walls"];
                life -= damage;
                if(life <= 0){
                    this.gameObject.SetActive(false);
                }
                break;
            case 8:
                damage = DamageTypes.Instance.damageDictionary["SmallEnemies"];
                life -= damage;
                if(life <= 0){
                    this.gameObject.SetActive(false);
                }
                break;
            case 9:
                damage = DamageTypes.Instance.damageDictionary["MediumEnemies"];
                life -= damage;
                if(life <= 0){
                    this.gameObject.SetActive(false);
                }
                break;
            case 10:
                damage = DamageTypes.Instance.damageDictionary["BigEnemies"];
                life -= damage;
                if(life <= 0){
                    this.gameObject.SetActive(false);
                }
                break;
            case 11:
                damage = DamageTypes.Instance.damageDictionary["Bosses"];
                life -= damage;
                if(life <= 0){
                    this.gameObject.SetActive(false);
                }
                break;
            default:
                break;
        }
    }
}
