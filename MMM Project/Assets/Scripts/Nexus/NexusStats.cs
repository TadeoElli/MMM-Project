using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusStats : MonoBehaviour
{
    public Observer<float> currentEnergy = new Observer<float>(1000f);
    //public float currentEnergy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float structure;
    [SerializeField] private float baseStability;
    [SerializeField] private int missilesUnlocked;
    [SerializeField] private int lives;
    private float energycost;
    private bool hasQueue = false;

    private void Start() {
        currentEnergy.Invoke();
    }
    private void Update() {
        currentEnergy.Value = currentEnergy.Value + 40 * Time.deltaTime;
        currentEnergy.Value = Mathf.Clamp(currentEnergy.Value,0,1000);
    }
    public void SetEnergyValue(float amount){
        currentEnergy.Value = amount;
    }



}
