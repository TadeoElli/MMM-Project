using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusCollisions : MonoBehaviour
{
    public Observer<float> currentStructure = new Observer<float>(3000);
    [SerializeField] private float damage;


    private void OnTriggerEnter2D(Collider2D other) {

    }

    public void TakeDamage(MissileStrategy creator){
        damage = DamageTypesForNexus.Instance.missilesDictionary[creator];
        Debug.Log("Recibi "+ damage+ " de dano" );
        currentStructure.Value -= damage;
    }

    public void SetStructureValue(float amount){
        currentStructure.Value = amount;
    }
        
}
