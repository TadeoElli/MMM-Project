using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Nexus : MonoBehaviour
{
    /// <summary>
    /// Esta clase se encarga de manejar el nexo, creando los misiles correspondientes al indice, lanzandolos, consumiendo la energia
    /// correspondiente al misil a las estadisticas generales
    /// </summary>
    public static Nexus Instance { get; private set; }
    public Observer<float> currentEnergy = new Observer<float>(1000f);      //la energia que tiene actualmente el nexo
    public Observer<float> currentSpeed = new Observer<float>(0.0f);      //la velocidad con la que se va a lanzar el misil
    public Observer<float> currentDistance = new Observer<float>(0.0f);      //la distancia del mouse con el nexo
    public Observer<float> currentStability = new Observer<float>(100f);      //la estabilidad actual con la que se podra lanzar un misil
    [Header("Misiles")]
    [SerializeField] private MissileStrategy [] missiles;   //La lista de misiles que puede spawnear el nexo
    [SerializeField] private GameObject mouseOverMissile, missilePrefab;
    [SerializeField] public int index, powerIndex, towerIndex;  //Toma los indices de los demas controladores para que no puedas lanzar un misil si tenes un poder o torre activo
    [SerializeField] private int baseStability,baseSpeed;   //La estabilidad y velocidad base que aumenta con los niveles
    [SerializeField] private bool haveMissile;  //Si tiene un misil creado
    [SerializeField] private bool pauseState;
    private bool isDraggin; //Si el mouse esta encima del nexo
    [SerializeField] private CircleCollider2D collider1;
    private NexusModel model;   //Clase que se encarga del feedback visual del nexo
    [SerializeField] private EnergyIndicator indicator;
    public UnityEvent resetCooldownsHud;
    [Header("Sounds Effects")]
    [SerializeField] private AudioClip invalidEffect;
    Camera cam;
    Vector2 force;
    [SerializeField] private Vector2 minPower, maxPower;
    Vector3 startPoint, endPoint;


    private void Awake() {  //Empieza con el indice en 0, establece la camara principal y busca los componentes que necesita
        cam = Camera.main;
        model = GetComponentInChildren<NexusModel>();
        if (Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    //Establece la posicion del nexo y del cursor y luego crea un misil, despues de un delay
    void Start()
    {
        haveMissile = false;
        pauseState = true;
        index = 0;
        mouseOverMissile.SetActive(false);
    }
    public void StartState(){
        pauseState = false;
        mouseOverMissile.transform.position = transform.position;
        StartCoroutine(DelayForSpawn());
    }
    private void Update() {
        if(!pauseState){
            if(isDraggin){
                if(Input.GetMouseButton(0)){        //Muevo el misil y el cursor a donde esta el mouse y agrando el collider del nexo
                    Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                    currentPoint.z = 0;
                    Vector3 direction = currentPoint - startPoint;
                    if(direction.magnitude > 2f){
                        direction = direction.normalized * 2f;
                        currentPoint = startPoint + direction;
                    }
                    MouseHoldBehaviour(currentPoint);
                    ShowFeedback();
                }
                if(Input.GetMouseButtonUp(0)){      //Marco el punto donde se solto para calcular el disparo
                    if(UseEnergy(missiles[index].energyConsumption)){   //Pregunto si tengo esa energia 
                    //Si la tengo, marco el punto donde se solto y pruebo lanzar el misil, luego activo su collider
                        endPoint = missilePrefab.transform.position;
                        endPoint.z = 0;
                        ShootMissile();
                        //Luego restablezco los valores del nexo y creo un nuevo misil desp del delay
                        NexusRestore();
                        haveMissile = false;
                        StartCoroutine(DelayForSpawn());    //Llamo a la couroutina para que cree otro misil despues de determinado tiempo
                    }
                    else{   //Si no tengo la energia para lanzarlo reseteo su posicion
                        NexusRestore();
                        AudioManager.Instance.PlaySoundEffect(invalidEffect);
                        missilePrefab.transform.position = startPoint;
                    }
                }
            }
        }
    }
    //Crea un misil y lo guarda en el missilePrefab, luego toma su collider y lo desactiva para activarlo cuando lo lanze, y cambia el color del cursor
    IEnumerator DelayForSpawn(){
        yield return new WaitForSeconds(2);
        missilePrefab = missiles[index].CreateMissile(transform);
        missilePrefab.GetComponent<Collider2D>().enabled = false;
        model.ChangeNexusModel(missiles[index].color,missiles[index].sprite, missiles[index].texture);
        haveMissile = true;
    }
    //Cuando el mouse este sobre el nexo
    private void OnMouseOver() {
        if(haveMissile && powerIndex == 0 && towerIndex == 0 && !pauseState){  //Si tiene un misil activo y ningun poder o torre activo
            ShowFeedback();
            if(Input.GetMouseButtonDown(0)){        //Marco el punto de origen
                startPoint = transform.position;
                startPoint.z = 0;
                isDraggin = true;
            }
        }
    }
    private bool UseEnergy(float amount){   //Si tengo la energia suficiente para lanzarlo, le resto la energia
        if(currentEnergy.Value >= amount ){
            currentEnergy.Value -= amount;
            return true;
        }
        else{
            return false;
        }
    }
    

    public void SetEnergyValue(float amount){   //Esta funcion recibe el valor de la energia del nexusStats y lo guarda, y al cambiar 
    //el valor de la energia, le manda ese valor al nexusStats para que lo actualize
        currentEnergy.Value = amount;
    }
    public void SetStabilityValue(int amount){  //Guarda la estabilidad base del NexusStats
        baseStability = amount;
    }
    public void SetSpeedValue(int amount){  //guardo la velocidad Base del nexusStats
        baseSpeed = amount;
    }
    private void OnMouseExit() {
        if(!isDraggin)
            HideFeedback();
    }
    private void HideFeedback() {
        mouseOverMissile.SetActive(false);
        indicator.gameObject.SetActive(false);
    }

    public void SetMissileIndex(int newIndex){  //Recive el indice del NexusStats y modifica el indice local
        index = newIndex;
        if(haveMissile){    //Y si tenia un misil activo en ese momento, lo desactivo y activo el nuevo con el nuevo indice
            missilePrefab.SetActive(false);
            missilePrefab = missiles[index].CreateMissile(transform);
            missilePrefab.GetComponent<Collider2D>().enabled = false;
        }
        model.ChangeNexusModel(missiles[index].color,missiles[index].sprite, missiles[index].texture);
    }
    public void SetPowerIndex(int newIndex){
        powerIndex = newIndex;
    }
    public void SetTowerIndex(int newIndex){
        towerIndex = newIndex;
    }

    private void ShowFeedback(){
        mouseOverMissile.SetActive(true);   //Activo el feedback del mouse
        indicator.gameObject.SetActive(true);
        indicator.SetPosition(missiles[index].energyConsumption);
    }
    private void MouseHoldBehaviour(Vector3 currentPoint){///Muevo los objetos en donde esta el mouse
        missilePrefab.transform.position = currentPoint;
        mouseOverMissile.transform.position = currentPoint;
        collider1.radius = 1f;          //Agrando el radio del nexo para que pueda alejar el cursor
        //Calculo la velocidad a la que saldra el proyectil y notifico a los suscriptores
        force = new Vector2(Mathf.Clamp(startPoint.x - currentPoint.x , minPower.x, maxPower.x),Mathf.Clamp(startPoint.y - currentPoint.y, minPower.y, maxPower.y));
        currentSpeed.Value = (force * ((missiles[index].velocity / 3) + baseSpeed)).magnitude;
        currentDistance.Value = Vector2.Distance(currentPoint,startPoint);
        currentStability.Value = CalculateStability(currentPoint);
    }

    private void ShootMissile(){
        //Fuerza = distancia entre el punto de inicio y el punto final, clampeado a los valores minimos y maximos de distancia
        force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x , minPower.x, maxPower.x),Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
        missilePrefab.GetComponent<Rigidbody2D>().AddForce(force * ((missiles[index].velocity / 3) + baseSpeed), ForceMode2D.Impulse);     //Tomo el rb del misil y le aplico fuerza
        missilePrefab.GetComponent<MissileBehaviour>().TryToShoot(startPoint,endPoint, baseStability);  //Pruebo a lanzar el misil
        missilePrefab.GetComponent<Collider2D>().enabled = true;    //Y activo su collider

        resetCooldownsHud?.Invoke();
    }
    private void NexusRestore(){
        collider1.radius = 0.2f;    //Vuelvo al collider chico
        mouseOverMissile.transform.position = transform.position;   //El cursor del misil lo vuelvo a la posicion inicial
        currentSpeed.Value = 0;     //Vuelvo el valor de la velocidad a 0
        currentDistance.Value = 0;
        currentStability.Value = 0;
        HideFeedback();
        isDraggin = false;
    }

    public float CalculateStability(Vector2 currentPoint){   
        float distance = Vector2.Distance(startPoint, currentPoint);
        float maxProbability = distance <= 1 ? missiles[index].maxStability + (baseStability * 3.5f) :
            Mathf.Clamp((missiles[index].maxStability + (missiles[index].minStability - missiles[index].maxStability) * (distance - 1)) + (baseStability * 3.5f),
                missiles[index].minStability, missiles[index].maxStability);
        return Mathf.Clamp(maxProbability, 0f, 100f);
    }

    private void OnDisable() {//Desactiva a todos los suscriptores y los objetos
        DisableNexus();
    }
    public void DisableNexus(){
        currentEnergy.RemoveAllListeners();
        currentSpeed.RemoveAllListeners();
        currentDistance.RemoveAllListeners();
        currentStability.RemoveAllListeners();
        if(missilePrefab != null){missilePrefab.SetActive(false);}
        if(mouseOverMissile != null){mouseOverMissile.SetActive(false);}
        if(indicator != null){indicator.gameObject.SetActive(false);}
        this.gameObject.SetActive(false);
        
    }
}
