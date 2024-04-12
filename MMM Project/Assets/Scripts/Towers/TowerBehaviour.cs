using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    [SerializeField] protected TowerStrategy tower;   //el strategy de la torre
    [SerializeField] protected float energy;  //la energia que tiene actualmente
    [SerializeField] protected bool hasSomethingInside = false;  //El flag para saber si tiene algo dentro de su collider
    [SerializeField] private GameObject towerFeedback;      //El feedback que va a tener cuando esta activo
    [SerializeField] private TowerView view;
    

    private void OnEnable() {   //Se reestablece la energia
        energy = tower.maxEnergy;
        view.SetMaxAmount(energy);
    }
    // Update is called once per frame
    void Update()
    {
        if(energy <= 0){    //Si la energia es menor a 0 se destruye la torre
            DestroyTower();
        }
        else{   //Si tiene algun objeto dentro del collider, la energia se disminuira 3 veces mas rapido y el componente feedback estara activo
            if(hasSomethingInside){
                energy = energy - 3 * Time.deltaTime;
                if(towerFeedback != null){
                    towerFeedback.SetActive(true);
                }
            }
            else{   //Si no la energia se disminuira y el componente feedback estara desactivado
                energy = energy - 1 * Time.deltaTime;
                if(towerFeedback!= null){
                    towerFeedback.SetActive(false);
                }
            }
            view.SetCurrentAmount(energy);
        }

    }
    private void DestroyTower(){    //Se instancia la explosion de la torre y se crea donde estaba la torre y se llama al comportamiento de la torre cuando se va  adestruir
        GameObject explosion = ExplosionPool.Instance.RequestExplosion(tower.explosion);
        explosion.transform.position = transform.position;
        tower.DestroyTower(this.gameObject);
        this.gameObject.SetActive(false);

    }
    private void OnTriggerEnter2D(Collider2D other) {   //Si un componente entro en su collider
        if(tower.ColliderBehaviour(this.gameObject,other.gameObject)){  //Se activa el comportamiento de collider y se reduce la energia adecuada
            hasSomethingInside = true;
            ReduceEnergy();
        }
    }
    //Si hay un objeto dentro del collider se llama al comportamiento especial
    private void OnTriggerStay2D(Collider2D other) {
        tower.SpecialBehaviour(this.gameObject, other.gameObject);
        if(tower.ColliderBehaviour(this.gameObject,other.gameObject)){
            hasSomethingInside = true;
        }
    }
    //Se establece que no hay ningun objeto dentro del collider
    private void OnTriggerExit2D(Collider2D other) {
        hasSomethingInside = false;
    }
    //Se reduce la energia adecuada
    private void ReduceEnergy(){
        energy -= tower.energyConsumption;
        
    }
}
