using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TowerView : MonoBehaviour 
{
    [SerializeField] private Image energyBar, range;
    private float currentEnergy, maxEnergy;

    public void Update(){
        energyBar.fillAmount = currentEnergy / maxEnergy;
    }
    private void OnMouseEnter() {
        energyBar.gameObject.SetActive(true);
        range.gameObject.SetActive(true);
    }
    private void OnMouseExit() {
        energyBar.gameObject.SetActive(false);
        range.gameObject.SetActive(false);
    }
    public void SetMaxAmount(float amount) {
        maxEnergy = amount;
    }
    public void SetCurrentAmount(float amount) {

        currentEnergy = amount;
    }
}