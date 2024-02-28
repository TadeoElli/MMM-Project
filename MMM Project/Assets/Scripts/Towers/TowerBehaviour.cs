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
            this.gameObject.SetActive(false);
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

    private void OnTriggerEnter2D(Collider2D other) {
        if(tower.ColliderBehaviour(this.gameObject,other.gameObject)){
            hasEnemyInside = true;
            ReduceEnergy();
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        tower.SpecialBehaviour(this.gameObject, other.gameObject);
    }
    private void OnTriggerExit2D(Collider2D other) {
        hasEnemyInside = false;
    }
    private void ReduceEnergy(){
        energy -= tower.energyConsumption;
        
    }
}
