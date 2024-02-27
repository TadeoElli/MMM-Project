using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    [SerializeField] private TowerStrategy tower;
    [SerializeField] private float radius, energy;
    

    private void OnEnable() {
        radius = tower.radius;
        energy = tower.maxEnergy;
    }
    // Update is called once per frame
    void Update()
    {
        if(energy <= 0){
            this.gameObject.SetActive(false);
        }
        else{
            energy = energy - 1 * Time.deltaTime;
            
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        tower.ColliderBehaviour(this.gameObject,other.gameObject);
        ReduceEnergy();
    }

    private void OnTriggerStay2D(Collider2D other) {
        tower.SpecialBehaviour(this.gameObject);
    }
    private void ReduceEnergy(){
        energy -= tower.energyConsumption;
        
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
