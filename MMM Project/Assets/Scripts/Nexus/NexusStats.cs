using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusStats : MonoBehaviour
{
    public Observer<float> currentEnergy = new Observer<float>(1000f);
    public Observer<float> currentStructure = new Observer<float>(3000f);
    public Observer<int> currentLives = new Observer<int>(35);
    //public float currentEnergy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float maxStructure;
    [SerializeField] private float baseStability;
    [SerializeField] private int missilesUnlocked;
    [SerializeField] private int maxLives;

    private void Start() {
        currentEnergy.Invoke();
        currentStructure.Invoke();
    }
    private void Update() {
        currentStructure.Value = currentStructure.Value + 20 * Time.deltaTime;
        currentStructure.Value = Mathf.Clamp(currentStructure.Value,0,maxStructure);
        currentEnergy.Value = currentEnergy.Value + 40 * Time.deltaTime;
        currentEnergy.Value = Mathf.Clamp(currentEnergy.Value,0,maxEnergy);
    }
    public void SetEnergyValue(float amount){
        currentEnergy.Value = amount;
    }

    public void SetStructureValue(float amount){
       // Debug.Log(currentStructure.Value);
        currentStructure.Value = amount;
    }



}
