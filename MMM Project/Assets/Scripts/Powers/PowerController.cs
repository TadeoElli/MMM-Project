using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


public class PowerController : MonoBehaviour 
{
    // Start is called before the first frame update
    public Observer<float> currentEnergy = new Observer<float>(1000f);  //La energia actual que se comparte con el nexusStats
    public Observer<int> currentIndex = new Observer<int>(0);   //El indice del poder que esta activo
    public Observer<bool> currentState = new Observer<bool>(false); //El estado en el que se encuentra el poder
    Camera cam;
    [SerializeField] private bool hasPower = false; //si tiene un poder activo el controllador
    private bool isDraggin = false; //Si esta activo y presionado
    [SerializeField] private PowerStrategy [] powers;   //La lista de poderes
    
    [SerializeField] private List<float> cooldowns; //la lista de cooldowns
    [SerializeField] private List<float> currentCd; //la lista de temporizadores para ver si ya se puede volver a usar
    [SerializeField] private List<bool> isReady;    //La lista de flags para saber si ya se pueden usar o no
    [SerializeField] private EnergyIndicator indicator;
    [SerializeField] private List<UnityEvent> changePowersHud;

    private void Awake() {
        cam = Camera.main;
    }
    void Start()
    //Establezco segun la cantidad de poderes, los cooldowns y flags en las listas (Dejando el del indice 0 vacio)
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

    //Por cada uno de los poderes pregunta si no esta listo, y si es asi aumenta el timer hasta que supere el cooldown para que se ponga listo
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
    //Si tiene un poder activo y se esta presionando, llama al comportamiento de presionado
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
    //Se setea los cooldowns restandoles el valor base
    public void SetCooldowns(float baseCooldown){
        for (int i = 1; i < powers.Length; i++)
        {
            cooldowns[i] = (powers[i].cooldown - baseCooldown);
            //currentCd[i] = (powers[i].cooldown);
        }
    }
    
    //Se setea el indice segun si se presiona alguna de las teclas o no, si el indice es mayor a 0 y ese poder esta activo, se activa el cursor
    //Si no se vuelve el indice a 0
    public void SetPowerIndex(int newIndex){
        if(!isReady[newIndex]){
            currentIndex.Value = 0;
            hasPower = false;
        }
        else{
            currentIndex.Value = newIndex;
            hasPower = true;
            indicator.gameObject.SetActive(true);
            indicator.SetPosition(powers[newIndex].energyConsumption);
            CursorController.Instance.SetCursor(powers[newIndex].sprite, powers[newIndex].material, powers[newIndex].scale);
        }
    }


    //Se modifica la energia para notificar a los suscriptores
    public void SetEnergyValue(float amount){
        currentEnergy.Value = amount;
    }
    //Si se tiene un poder activo y se cumple con el requisito de energia, se llama al comportamiento de inicio del poder correspondiente
    //Si este tiene un comportamiento para cuando se mantiene presionado entonces devuelve true y se establece que se esta activando constantemente
    //Si no entonces luego del comportamiento de inicio se llama a DesactivatePower
    //Si se solto el click, se llama al comportamiento FinishPower
    public void ActivatePower(InputAction.CallbackContext callbackContext){
        if(hasPower){
            if(currentEnergy.Value >= powers[currentIndex.Value].energyConsumption && CheckDistance()){
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
            else{
                AudioManager.Instance.PlaySoundEffect(powers[currentIndex.Value].invalidEffect);
            }
        }
    }
    private bool CheckDistance(){
        Vector2 currentPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        if(currentPosition.y > -3.7f && currentPosition.y < 4.85f){
            return true;
        }
        else{
            AudioManager.Instance.PlaySoundEffect(powers[currentIndex.Value].invalidEffect);
            return false;
        }
    }
    //Se habilita el ingreso de nuevos inputs, se llama al comportamiento de cuando se suelta el click y se resetea los valores de cooldown del poder
    //Se resetea el indice, se resta la energia correspondiente
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
        changePowersHud[currentIndex.Value]?.Invoke();
        currentIndex.Value = 0;
        CursorController.Instance.RestoreCursor();
        indicator.gameObject.SetActive(false);
    }

    //Se habilita el ingreso de nuevos inputs, se establece que no hay un poder activo y se resetea el indice y el cursor
    public void DesactivatePower(){
        if(hasPower){
            InputController inputController = FindObjectOfType<InputController>();
            inputController.isAvailable = true;
            hasPower = false;
            currentIndex.Value = 0;
            CursorController.Instance.RestoreCursor();
            indicator.gameObject.SetActive(false);
        }
    }


}

