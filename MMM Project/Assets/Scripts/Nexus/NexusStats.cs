using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NexusStats : MonoBehaviour
{
    /// <summary>
    /// Esta clase se encarga de manejar las estadisticas del nexo, notificar a los suscriptores de su valor actual y modificarlas si se usa un power up
    /// </summary>
    #region Observers
    public Observer<float> maxEnergy = new Observer<float>(1000f);  //La energia maximaActual
    public Observer<float> currentEnergy = new Observer<float>(1000f);  //La energia actual
    public Observer<float> currentStructure = new Observer<float>(3000f);   //La vida del nexo actual
    public Observer<float> maxStructure = new Observer<float>(3000f);   //La cantidad de vida maxima
    public Observer<float> currentBaseCooldown = new Observer<float>(0f);   //la cantidad de tiempo que se reduce de las habilidades
    public Observer<int> currentLives = new Observer<int>(35);  //La cantidad de vidas
    public Observer<int> currentBaseStability = new Observer<int>(0);   //La estabilidad base
    public Observer<int> currentBaseSpeed = new Observer<int>(0);   //La velocidad base
    public Observer<int> currentLevel = new Observer<int>(1);   //La velocidad base
    #endregion
    #region Properties
    //public float currentEnergy;
    [SerializeField] private int baseStability; //La estabilidad base
    [SerializeField] private int baseSpeed; //La velocidad base
    [SerializeField] private int baseEnergy;    //La cantidad de energia base
    [SerializeField] private int missilesUnlocked;  //Que misiles estan desbloqueados
    [SerializeField] private int maxLives;  //La cantidad de vida maxima
    [SerializeField] private int startTechLevel;  //el nivel de tecnologia con el que se empieza
    [SerializeField] GameObject loseMenu;   //El menu de derrota
    private bool isDestroyed = false;
    
    [Header("PowerUpStats")]
    [SerializeField] private int energyRegen = 40;   //La regeneracion de energia
    [SerializeField] private int boostEnergyRegen = 100;
    private bool energyBoost = false;
    [SerializeField] private int structureRegen = 20;    //La regeneracion de vida
    [SerializeField] private int boostStructureRegen = 80;
    private bool structureBoost = false;
    [SerializeField] private float boostCooldown = 50f;
    [SerializeField] private UnityEvent<int> cooldownPowerUp, speedPowerUp, structurePowerUp, energyPowerUp, stabilityPowerUp;
    #endregion

//Estableze los valores iniciales y notifica a todos los suscriptores
    private void Start() {
        InitializeStats();
        InvokeEvents();
    }

    //Si todavia no se destruyo, regenera constantemente la vida y energia y si la vida baja de 0, remueve a todos los suscrptores
    private void Update() {
        RegenerateStats();
        CheckDestroyCondition();
    }
    private void InitializeStats()
    {
        currentLives.Value = maxLives;
        maxEnergy.Value += baseEnergy * 35;
        currentEnergy.Value = maxEnergy.Value;
        currentBaseStability.Value = baseStability;
        currentBaseSpeed.Value = baseSpeed;
        currentLevel.Value = startTechLevel;
    }
    private void InvokeEvents()
    {
        currentLives.Invoke();
        currentEnergy.Invoke();
        currentStructure.Invoke();
        currentBaseStability.Invoke();
        currentBaseSpeed.Invoke();
        currentBaseCooldown.Invoke();
        currentLevel.Invoke();
        maxEnergy.Invoke();
        maxStructure.Invoke();
    }
    private void RegenerateStats()
    {
        if (!isDestroyed)
        {
            currentStructure.Value += structureBoost ? boostStructureRegen * Time.deltaTime : structureRegen * Time.deltaTime;
            currentStructure.Value = Mathf.Clamp(currentStructure.Value, 0, maxStructure.Value);

            currentEnergy.Value += energyBoost ? boostEnergyRegen * Time.deltaTime : energyRegen * Time.deltaTime;
            currentEnergy.Value = Mathf.Clamp(currentEnergy.Value, 0, maxEnergy.Value);
        }
    }
    private void CheckDestroyCondition()
    {
        if (!isDestroyed && (currentStructure.Value <= 0 || currentLives.Value <= 0))
        {
            DestroyNexus();
        }
    }
    private void DestroyNexus()
    {
        isDestroyed = true;
        loseMenu.SetActive(true);
        RemoveAllListeners();
    }
    private void RemoveAllListeners()
    {
        currentEnergy.RemoveAllListeners();
        currentStructure.RemoveAllListeners();
        currentBaseCooldown.RemoveAllListeners();
        currentLives.RemoveAllListeners();
        currentBaseStability.RemoveAllListeners();
        currentBaseSpeed.RemoveAllListeners();
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
        energyBoost = true;
        energyPowerUp?.Invoke(cooldown);
        Invoke("RestoreEnergy",cooldown);
    }
    public void StructurePowerUp(int cooldown){ //guarda la antigua regeneracion de vida y la actual pasa a ser mayor
        structureBoost = true;
        structurePowerUp?.Invoke(cooldown);
        Invoke("RestoreStructure",cooldown);
    }
    public void StabilityPowerUp(int cooldown){ //la actual estabilidad pasa a ser mayor
        currentBaseStability.Value = 35;
        stabilityPowerUp?.Invoke(cooldown);
        Invoke("RestoreStability",cooldown);
    }
    public void SpeedPowerUp(int cooldown){ //la actual velocidad  pasa a ser mayor
        currentBaseSpeed.Value = 10;
        speedPowerUp?.Invoke(cooldown);
        Invoke("RestoreSpeed",cooldown);
    }
    public void CooldownPowerUp(int cooldown){   //guarda el antiguo valor que se reduce a los cooldowns y la actual pasa a ser mayor
        currentBaseCooldown.Value = boostCooldown;
        cooldownPowerUp?.Invoke(cooldown);
        Invoke("RestoreCooldown",cooldown);
    }


    private void RestoreEnergy(){   //Restaura el valor default de regeneracion dew energia
        energyBoost = false;
    }
    private void RestoreStructure(){    //Restaura el valor default de regeneracion de vida
        structureBoost = false;
    }
    private void RestoreStability(){    //Restaura el valor default de estabilidad
        currentBaseStability.Value = baseStability;
    }
    private void RestoreSpeed(){    //Restaura el valor default de velocidad
        currentBaseSpeed.Value = baseSpeed;
    }
    private void RestoreCooldown(){ //Restaura el valor default de reduccion de cooldown
        currentBaseCooldown.Value = 0f;
    }
    #endregion

}
