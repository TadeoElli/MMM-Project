using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusStats : MonoBehaviour
{
    public Observer<float> currentEnergy = new Observer<float>(1000f);
    public Observer<float> currentStructure = new Observer<float>(3000f);
    public Observer<float> currentCooldown = new Observer<float>(0f);
    public Observer<int> currentLives = new Observer<int>(35);
    public Observer<int> currentBaseStability = new Observer<int>(0);
    public Observer<int> currentBaseSpeed = new Observer<int>(0);
    //public float currentEnergy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float maxStructure;
    [SerializeField] private int baseStability;
    [SerializeField] private int baseSpeed;
    [SerializeField] private int baseEnergy;
    [SerializeField] private int missilesUnlocked;
    [SerializeField] private int maxLives;
    private int energyRegen = 40;
    private int structureRegen = 20;

    private int oldEnergyRegen, oldStructureRegen;
    private float oldCooldown;
    private bool isDestroyed = false;

    private void Start() {
        maxEnergy = maxEnergy + (baseEnergy * 35);
        currentEnergy.Value = maxEnergy;
        currentEnergy.Invoke();
        currentStructure.Invoke();
        currentBaseStability.Value = baseStability;
        currentBaseStability.Invoke();
        currentBaseSpeed.Value = baseSpeed;
        currentBaseSpeed.Invoke();
        currentCooldown.Invoke();
    }
    private void Update() {
        if(!isDestroyed){
            currentStructure.Value = currentStructure.Value + 20 * Time.deltaTime;
            currentStructure.Value = Mathf.Clamp(currentStructure.Value,-500f,maxStructure);
            currentEnergy.Value = currentEnergy.Value + energyRegen * Time.deltaTime;
            currentEnergy.Value = Mathf.Clamp(currentEnergy.Value,0,maxEnergy);
            if(currentStructure.Value <= 0 ){
                currentEnergy.RemoveAllListener();
                currentStructure.RemoveAllListener();
                isDestroyed = true;
            }
        }
    }
    public void SetEnergyValue(float amount){
        currentEnergy.Value = amount;
    }

    public void SetStructureValue(float amount){
       // Debug.Log(currentStructure.Value);
        currentStructure.Value = amount;
    }

    #region PowersUp
    public void EnergyPowerUp(int cooldown){
        oldEnergyRegen = energyRegen; 
        energyRegen =  100;
        Invoke("RestoreEnergy",cooldown);
    }
    public void StructurePowerUp(int cooldown){
        oldStructureRegen = structureRegen; 
        structureRegen =  100;
        Invoke("RestoreStructure",cooldown);
    }
    public void StabilityPowerUp(int cooldown){
        currentBaseStability.Value = 35;
        Invoke("RestoreStability",cooldown);
    }
    public void SpeedPowerUp(int cooldown){
        currentBaseSpeed.Value = 25;
        Invoke("RestoreSpeed",cooldown);
    }
    public void CooldownPowerUp(int cooldown){
        oldCooldown = currentCooldown.Value;
        currentCooldown.Value = 50;
        Invoke("RestoreCooldown",cooldown);
    }


    private void RestoreEnergy(){
        energyRegen = oldEnergyRegen;
    }
    private void RestoreStructure(){
        structureRegen = oldStructureRegen;
    }
    private void RestoreStability(){
        currentBaseStability.Value = baseStability;
    }
    private void RestoreSpeed(){
        currentBaseSpeed.Value = baseSpeed;
    }
    private void RestoreCooldown(){
        currentCooldown.Value = oldCooldown;
    }
    #endregion

}
