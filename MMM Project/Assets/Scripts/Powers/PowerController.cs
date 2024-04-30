using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Linq;

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
        //IA2-LINQ
        //le envio las listas de cooldowns y currentCd al generator que se encarga de crear la lista con los valores de la lista de poderes
        //restandole el valor indicado y dejando el primer objeto de la lista como nulo
        cooldowns = powers.SetCooldownsValue(x => x.cooldown - 0).ToList();
        currentCd = powers.SetCooldownsValue(x => x.cooldown - 0).ToList();
        //de misma forma en la lista isReady se crea una nueva colocando false en el primer indice y luego concatenandola con otra que
        //por cada poder en la lista de poderes (saltenado la primer posicion que ya se declaro como false) estableciemdolas como true
        isReady = new List<bool> { false }
        .Concat(powers.Skip(1).Select(power => true))
        .ToList();
    }

    //Por cada uno de los poderes pregunta si no esta listo, y si es asi aumenta el timer hasta que supere el cooldown para que se ponga listo
    void Update()
    {
        //IA2-LINQ
        //creo una nueva lista del mismo tamaño que la de poderes pero guardando solo los indices donde el poder no esta listo
        var notReadyIndices = Enumerable.Range(1, powers.Length - 1)
        .Where(i => !isReady[i])
        .ToList();
        //Y por cada poder con ese indice le sumo al currentCD el valor del tiempo hasta que sea mayor al cooldown y ahi lo establezco como activo
        foreach (var index in notReadyIndices)
        {
            currentCd[index] = Mathf.Clamp(currentCd[index] + Time.deltaTime, 0, cooldowns[index]);
            if (currentCd[index] >= cooldowns[index])
            {
                isReady[index] = true;
            }
        }
     
        
    }
    //Si tiene un poder activo y se esta presionando, llama al comportamiento de presionado
    private void FixedUpdate() {
        if(hasPower && isDraggin){
            if(!powers[currentIndex.Value].BehaviourPerformed()){
                FinishPower();
            }else{
                if(powers[currentIndex.Value].hasPerformedCursor){
                    currentState.Value = true;
                }
            }

        }
    }
    //Se setea los cooldowns restandoles el valor base
    //sirve para que cuando se utiliza el powerUp de reducir el cooldown, todos los cooldown pasen a menos de 0 para que se puedan usar sin esperar a q este listo
    public void SetCooldowns(float baseCooldown){
        //IA2-LINQ
        //le envio la listas de cooldowns al generator que se encarga de crear la lista con los valores de la lista de poderes
        //restandole el valor indicado y dejando el primer objeto de la lista como nulo
        cooldowns = powers.SetCooldownsValue(x => x.cooldown - baseCooldown).ToList();
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
        bool isValidDistance = currentPosition.y > -3.7f && currentPosition.y < 4.85f;
        if (!isValidDistance)
            AudioManager.Instance.PlaySoundEffect(powers[currentIndex.Value].invalidEffect);
        return isValidDistance;
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

