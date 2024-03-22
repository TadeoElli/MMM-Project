using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Observer<float> currentEnergy = new Observer<float>(1000f);
    public Observer<int> currentIndex = new Observer<int>(0);
    public Observer<bool> currentState = new Observer<bool>(false);
    Camera cam;
    [SerializeField] private bool hasPower = false;
    private bool isDraggin = false;

    [SerializeField] private PowerStrategy [] powers;
    
    [SerializeField] private List<float> cooldowns;
    [SerializeField] private List<float> currentCd;
    [SerializeField] private List<bool> isReady;
    private void Awake() {
        cam = Camera.main;
    }
    void Start()
    {
        for (int i = 0; i < powers.Length; i++)
        {
            if(i == 0){
                cooldowns.Add(0);
                currentCd.Add(0);
                isReady.Add(false);
            }
            else{
                cooldowns.Add(powers[i].cooldown);
                currentCd.Add(powers[i].cooldown);
                isReady.Add(true);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i < powers.Length; i++)
        {
            if(!isReady[i]){
                if(currentCd[i] >= cooldowns[i]){
                    isReady[i] = true;
                }
                else
                {
                    currentCd[i] = currentCd[i] + 1 * Time.deltaTime;
                    currentCd[i] = Mathf.Clamp(currentCd[i], 0, cooldowns[i]);
                }
            }
        }
        
    }

    private void FixedUpdate() {
        if(hasPower && isDraggin){
            if(!powers[currentIndex.Value].BehaviourPerformed()){
                Debug.Log("EnemyDestroyed");
                FinishPower();
            }else{
                if(powers[currentIndex.Value].hasPerformedCursor){
                    currentState.Value = true;
                }
            }

        }
    }

    public void SetPowerIndex(int newIndex){
        if(!isReady[newIndex]){
            currentIndex.Value = 0;
            hasPower = false;
        }
        else{
            currentIndex.Value = newIndex;
            hasPower = true;
        }
    }


    public void SetEnergyValue(float amount){
        currentEnergy.Value = amount;
    }

    public void ActivatePower(InputAction.CallbackContext callbackContext){
        if(hasPower && currentEnergy.Value >= powers[currentIndex.Value].energyConsumption){
            if(callbackContext.started){
                if(powers[currentIndex.Value].BehaviourStarted()){
                    isDraggin = true;
                }
                else{
                    DesactivatePower();
                }
            }
            else if(callbackContext.canceled){
                FinishPower();
            }
        }
    }
    private void FinishPower(){
        InputController inputController = FindObjectOfType<InputController>();
        inputController.isAvailable = true;
        powers[currentIndex.Value].BehaviourEnded();
        isReady[currentIndex.Value] = false;
        currentCd[currentIndex.Value] = 0;
        currentEnergy.Value = currentEnergy.Value - powers[currentIndex.Value].energyConsumption;
        isDraggin = false;
        if(powers[currentIndex.Value].hasPerformedCursor){
            currentState.Value = false;
        }
        currentIndex.Value = 0;
    }

    public void DesactivatePower(){
        if(hasPower){
            hasPower = false;
            currentIndex.Value = 0;
        }
    }


}

