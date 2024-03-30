using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusCollisions : MonoBehaviour
///Esta clase guarda la vida del nexo y maneja sus collisiones, si una explosion surge dentro del area, le manda a esta clase el tipo de explosion
///y esta calcula y reduce la vida segun el daño correspondiente, si se queda sin vida, se destruye el nexo
{
    public Observer<float> currentStructure = new Observer<float>(3000);
    [SerializeField] private float damage;




    public void TakeDamageForMissile(ExplosionsTypes type){//Recibe el tipo de daño, pregunta cuanto es y lo resta de la vida del nexo
        damage = DamageTypes.Instance.explosionDictionary[type];
        Debug.Log("Recibi "+ damage+ " de dano" );
        currentStructure.Value -= damage;
        if(currentStructure.Value <= 0){
            DestroyNexus();
        }
    }

    public void SetStructureValue(float amount){    //Recibe el valor de vida actual del NexusStats y lo guarda en la variable de vida local
        currentStructure.Value = amount;
    }

    private void DestroyNexus() {
        this.gameObject.SetActive(false);
    }

    private void OnDisable() {
        currentStructure.RemoveAllListener();
    }
        
}
