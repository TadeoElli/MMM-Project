using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    [SerializeField] private TowerStrategy tower;
    [SerializeField] private float energy;
    [SerializeField] private bool hasEnemyInside = false;
    

    private void OnEnable() {
        energy = tower.maxEnergy;
    }
    // Update is called once per frame
    void Update()
    {
        if(energy <= 0){
            DestroyTower();
        }
        else{
            if(hasEnemyInside){
                energy = energy - 3 * Time.deltaTime;
            }
            else{
                energy = energy - 1 * Time.deltaTime;
            }
            
        }

    }
    private void DestroyTower(){
        GameObject explosion = ExplosionPool.Instance.RequestExplosion(tower.explosion);
        explosion.GetComponent<Explosion>().creatorId =  tower.id;
        explosion.transform.position = transform.position;
        tower.DestroyTower(this.gameObject);
        this.gameObject.SetActive(false);

    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(tower.ColliderBehaviour(this.gameObject,other.gameObject)){
            hasEnemyInside = true;
            ReduceEnergy();
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        tower.SpecialBehaviour(this.gameObject, other.gameObject);
        if(tower.ColliderBehaviour(this.gameObject,other.gameObject)){
            hasEnemyInside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        hasEnemyInside = false;
    }
    private void ReduceEnergy(){
        energy -= tower.energyConsumption;
        
    }
}
