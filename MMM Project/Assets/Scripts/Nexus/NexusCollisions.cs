using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusCollisions : MonoBehaviour
{
    public Observer<float> currentStructure = new Observer<float>(3000);


    private void OnTriggerEnter2D(Collider2D other) {
        switch (other.gameObject.tag)
        {
            case "Nexus":
                //damage = DamageTypesForNexus.Instance.damageDictionary["Missile01"];
                Debug.Log("Explosion");
                TakeDamage(200);
                break;
            default:
                break;
        }
    }

    public void TakeDamage(int damage){
        currentStructure.Value -= damage;
    }

    public void SetStructureValue(float amount){
        currentStructure.Value = amount;
    }
        
}
