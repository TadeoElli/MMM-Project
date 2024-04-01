using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : MonoBehaviour
{
    /// <summary>
    /// Esta clase se encarga de manejar el nexo, creando los misiles correspondientes al indice, lanzandolos, consumiendo la energia
    /// correspondiente al misil a las estadisticas generales
    /// </summary>
    public Observer<float> currentEnergy = new Observer<float>(1000f);      //la energia que tiene actualmente el nexo
    [Header("Misiles")]
    [SerializeField] private MissileStrategy [] missiles;   //La lista de misiles que puede spawnear el nexo
    [SerializeField] private GameObject mouseOverMissile, missilePrefab;
    [SerializeField] public int index, powerIndex, towerIndex;  //Toma los indices de los demas controladores para que no puedas lanzar un misil si tenes un poder o torre activo
    [SerializeField] private int baseStability,baseSpeed;   //La estabilidad y velocidad base que aumenta con los niveles
    [SerializeField] private bool haveMissile;  //Si tiene un misil creado
    [SerializeField] private CircleCollider2D collider1;
    private NexusPosition nexusPosition;   //Clase que se encarga de setear en donde va a estar el nexo
    private NexusModel model;   //Clase que se encarga del feedback visual del nexo


    Camera cam;
    Vector2 force;
    [SerializeField] private Vector2 minPower, maxPower;
    Vector3 startPoint, endPoint;


    private void Awake() {  //Empieza con el indice en 0, establece la camara principal y busca los componentes que necesita
        cam = Camera.main;
        nexusPosition = FindObjectOfType<NexusPosition>();
        model = GetComponentInChildren<NexusModel>();
    }
    //Establece la posicion del nexo y del cursor y luego crea un misil, despues de un delay
    void Start()
    {
        this.transform.position = nexusPosition.SetPosition();
        haveMissile = false;
        mouseOverMissile.SetActive(false);
        mouseOverMissile.transform.position = transform.position;
        index = 0;
        StartCoroutine(DelayForSpawn());
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
        if(haveMissile && powerIndex == 0 && towerIndex == 0){  //Si tiene un misil activo y ningun poder o torre activo
            mouseOverMissile.SetActive(true);   //Activo el feedback del mouse
            if(Input.GetMouseButtonDown(0)){        //Marco el punto de origen
                startPoint = transform.position;
                startPoint.z = 0;
            }
            if(Input.GetMouseButton(0)){        //Muevo el misil y el cursor a donde esta el mouse y agrando el collider del nexo
                Vector3 curreentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                curreentPoint.z = 0;
                missilePrefab.transform.position = curreentPoint;
                mouseOverMissile.transform.position = curreentPoint;
                collider1.radius = 1f;
            }
            if(Input.GetMouseButtonUp(0)){      //Marco el punto donde se solto para calcular el disparo
                float energyConsumption = missiles[index].energyConsumption;    //Guardo cuanta energia me cuesta lanzar el misil
                if(UseEnergy(energyConsumption)){   //Pregunto si tengo esa energia 
                //Si la tengo, marco el punto donde se solto y pruebo lanzar el misil, luego activo su collider
                    endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                    endPoint.z = 0;
                    //Fuerza = distancia entre el punto de inicio y el punto final, clampeado a los valores minimos y maximos de distancia
                    force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x , minPower.x, maxPower.x),Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
                    missilePrefab.GetComponent<Rigidbody2D>().AddForce(force * ((missiles[index].velocity / 3) + baseSpeed), ForceMode2D.Impulse);     //Tomo el rb del misil y le aplico fuerza
                    //Debug.Log((force * (missiles[index].velocity / 3)).magnitude);
                    missilePrefab.GetComponent<MissileBehaviour>().TryToShoot(startPoint,endPoint, baseStability);
                    missilePrefab.GetComponent<Collider2D>().enabled = true;
                    //Luego restablezco los valores del nexo y creo un nuevo misil desp del delay
                    collider1.radius = 0.2f;
                    haveMissile = false;
                    mouseOverMissile.transform.position = transform.position;
                    StartCoroutine(DelayForSpawn());
                }
                else{   //Si no tengo la energia para lanzarlo reseteo su posicion
                    missilePrefab.transform.position = startPoint;
                    mouseOverMissile.transform.position = startPoint;
                    collider1.radius = 0.2f;
                }
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
        mouseOverMissile.SetActive(false);
    }

    public void SetMissileIndex(int newIndex){  //Recive el indice del NexusStats y modifica el indice local
        index = newIndex;
        if(haveMissile){    //Y si tenia un misil activo en ese momento, lo desactivo y activo el nuevo con el nuevo indice
            missilePrefab.SetActive(false);
            missilePrefab = missiles[index].CreateMissile(transform);
            missilePrefab.GetComponent<Collider2D>().enabled = false;
            model.ChangeNexusModel(missiles[index].color,missiles[index].sprite, missiles[index].texture);
        }
    }
    public void SetPowerIndex(int newIndex){
        powerIndex = newIndex;
    }
    public void SetTowerIndex(int newIndex){
        towerIndex = newIndex;
    }




    private void OnDisable() {//Desactiva a todos los suscriptores y los objetos
        currentEnergy.RemoveAllListener();
        if(missilePrefab != null){missilePrefab.SetActive(false);}
        mouseOverMissile.SetActive(false);
    }
}
