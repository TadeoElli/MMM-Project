using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusCollisions : MonoBehaviour
{
    public Observer<float> currentStructure = new Observer<float>(3000);
    [SerializeField] private float damage;




    public void TakeDamageForMissile(int creatorId){
        damage = DamageTypes.Instance.explosionDictionary[creatorId];
        Debug.Log("Recibi "+ damage+ " de dano" );
        currentStructure.Value -= damage;
        if(currentStructure.Value <= 0){
            DestroyNexus();
        }
    }

    public void SetStructureValue(float amount){
        currentStructure.Value = amount;
    }

    private void DestroyNexus() {
        this.gameObject.SetActive(false);
    }

    private void OnDisable() {
        currentStructure.RemoveAllListener();
    }
        
}
