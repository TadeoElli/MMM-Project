using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusStats : MonoBehaviour
{
    /// <summary>
    /// Esta clase se encarga de manejar las estadisticas del nexo, notificar a los suscriptores de su valor actual y modificarlas si se usa un power up
    /// </summary>
    #region Observers
    public Observer<float> currentEnergy = new Observer<float>(1000f);  //La energia actual
    public Observer<float> currentStructure = new Observer<float>(3000f);   //La vida del nexo actual
    public Observer<float> currentBaseCooldown = new Observer<float>(0f);   //la cantidad de tiempo que se reduce de las habilidades
    public Observer<int> currentLives = new Observer<int>(35);  //La cantidad de vidas
    public Observer<int> currentBaseStability = new Observer<int>(0);   //La estabilidad base
    public Observer<int> currentBaseSpeed = new Observer<int>(0);   //La velocidad base
    public Observer<int> currentLevel = new Observer<int>(1);   //La velocidad base
    #endregion
    #region Properties
    //public float currentEnergy;
    [SerializeField] private float maxEnergy;   //La energia maxima
    [SerializeField] private float maxStructure;    //La cantidad de vida maxima
    [SerializeField] private int baseStability; //La estabilidad base
    [SerializeField] private int baseSpeed; //La velocidad base
    [SerializeField] private int baseEnergy;    //La cantidad de energia base
    [SerializeField] private int missilesUnlocked;  //Que misiles estan desbloqueados
    [SerializeField] private int maxLives;  //La cantidad de vida maxima
    [SerializeField] private int startTechLevel;  //el nivel de tecnologia con el que se empieza
    [SerializeField] GameObject loseMenu;   //El menu de derrota
    private int energyRegen = 40;   //La regeneracion de energia
    private int structureRegen = 20;    //La regeneracion de vida

    private int oldEnergyRegen, oldStructureRegen;  //Variables para guardar la antigua regeneracion
    private float oldCooldown;
    private bool isDestroyed = false;
    #endregion

//Estableze los valores iniciales y notifica a todos los suscriptores
    private void Start() {
        currentLives.Value = maxLives;
        maxEnergy = maxEnergy + (baseEnergy * 35);
        currentEnergy.Value = maxEnergy;
        currentBaseStability.Value = baseStability;
        currentBaseSpeed.Value = baseSpeed;
        currentLevel.Value = startTechLevel;
        currentLives.Invoke();
        currentEnergy.Invoke();
        currentStructure.Invoke();
        currentBaseStability.Invoke();
        currentBaseSpeed.Invoke();
        currentBaseCooldown.Invoke();
        currentLevel.Invoke();
    }

    //Si todavia no se destruyo, regenera constantemente la vida y energia y si la vida baja de 0, remueve a todos los suscrptores
    private void Update() {
        if(!isDestroyed){
            currentStructure.Value = currentStructure.Value + 20 * Time.deltaTime;
            currentStructure.Value = Mathf.Clamp(currentStructure.Value,-500f,maxStructure);
            currentEnergy.Value = currentEnergy.Value + energyRegen * Time.deltaTime;
            currentEnergy.Value = Mathf.Clamp(currentEnergy.Value,0,maxEnergy);
            if(currentStructure.Value <= 0  || currentLives.Value <= 0){
                currentEnergy.RemoveAllListener();
                currentStructure.RemoveAllListener();
                currentBaseCooldown.RemoveAllListener();
                currentLives.RemoveAllListener();
                currentBaseStability.RemoveAllListener();
                currentBaseSpeed.RemoveAllListener();
                isDestroyed = true;
                loseMenu.SetActive(true);
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
    public void ReduceLives(){
       // Debug.Log(currentStructure.Value);
        currentLives.Value--;
    }


    #region PowersUp
    public void EnergyPowerUp(int cooldown){    //guarda la antigua regeneracion de energia y la actual pasa a ser mayor
        oldEnergyRegen = energyRegen; 
        energyRegen =  100;
        Invoke("RestoreEnergy",cooldown);
    }
    public void StructurePowerUp(int cooldown){ //guarda la antigua regeneracion de vida y la actual pasa a ser mayor
        oldStructureRegen = structureRegen; 
        structureRegen =  100;
        Invoke("RestoreStructure",cooldown);
    }
    public void StabilityPowerUp(int cooldown){ //la actual estabilidad pasa a ser mayor
        currentBaseStability.Value = 35;
        Invoke("RestoreStability",cooldown);
    }
    public void SpeedPowerUp(int cooldown){ //la actual velocidad  pasa a ser mayor
        currentBaseSpeed.Value = 10;
        Invoke("RestoreSpeed",cooldown);
    }
    public void CooldownPowerUp(int cooldown){   //guarda el antiguo valor que se reduce a los cooldowns y la actual pasa a ser mayor
        oldCooldown = currentBaseCooldown.Value;
        currentBaseCooldown.Value = 50;
        Invoke("RestoreCooldown",cooldown);
    }


    private void RestoreEnergy(){   //Restaura el valor default de regeneracion dew energia
        energyRegen = oldEnergyRegen;
    }
    private void RestoreStructure(){    //Restaura el valor default de regeneracion de vida
        structureRegen = oldStructureRegen;
    }
    private void RestoreStability(){    //Restaura el valor default de estabilidad
        currentBaseStability.Value = baseStability;
    }
    private void RestoreSpeed(){    //Restaura el valor default de velocidad
        currentBaseSpeed.Value = baseSpeed;
    }
    private void RestoreCooldown(){ //Restaura el valor default de reduccion de cooldown
        currentBaseCooldown.Value = oldCooldown;
    }
    #endregion

}
