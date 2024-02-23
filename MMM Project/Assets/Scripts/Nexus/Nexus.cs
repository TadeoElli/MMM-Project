using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : MonoBehaviour, IObserver
{
    public Observer<float> currentEnergy = new Observer<float>(1000f);
    [SerializeField] Subject _inputController;
    [SerializeField] private MissileStrategy [] missiles;
    [SerializeField] private GameObject mouseOverMissile, missilePrefab;
    [SerializeField] private int index = 0;
    [SerializeField] private bool haveMissile;
    [SerializeField] private CircleCollider2D collider1;

    [Header("Trail")]
    public TrajectoryLine tl;
    Camera cam;
    Vector2 force;
    [SerializeField] private Vector2 minPower, maxPower;
    Vector3 startPoint, endPoint;


    private void Awake() {
        cam = Camera.main;
        tl = GetComponent<TrajectoryLine>();
    }
    void Start()
    {
        haveMissile = false;
        mouseOverMissile.SetActive(false);
        index = 0;
        StartCoroutine(DelayForSpawn());
    }

    

    IEnumerator DelayForSpawn(){
        yield return new WaitForSeconds(2);
        missilePrefab = missiles[index].CreateMissile(transform);
        missilePrefab.GetComponent<Collider2D>().enabled = false;
        haveMissile = true;
    }

    private void OnMouseOver() {
        if(haveMissile){
            mouseOverMissile.SetActive(true);   //Activo el feedback del mouse
            if(Input.GetMouseButtonDown(0)){        //Marco el punto de origen
                startPoint = transform.position;
                startPoint.z = 0;
            }
            if(Input.GetMouseButton(0)){        //Guardo la posicion del mouse para el trail y el feedback del  mouse
                Vector3 curreentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                curreentPoint.z = 0;
                missilePrefab.transform.position = curreentPoint;
                mouseOverMissile.transform.position = curreentPoint;
                collider1.radius = 1f;
                tl.RenderLine(startPoint, curreentPoint);       //hago el renderizado del trail
            }
            if(Input.GetMouseButtonUp(0)){      //Marco el punto donde se solto para calcular el disparo
                float energyConsumption = missiles[index].energyConsumption;
                if(UseEnergy(energyConsumption)){
                    endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                    endPoint.z = 0;
                    //Fuerza = distancia entre el punto de inicio y el punto final, clampeado a los valores minimos y maximos de distancia
                    force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x , minPower.x, maxPower.x),Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
                    missilePrefab.GetComponent<Rigidbody2D>().AddForce(force * 5, ForceMode2D.Impulse);     //Tomo el rb del misil y le aplico fuerza
                    missilePrefab.GetComponent<MissileBehavior>().TryToShoot(startPoint,endPoint);
                    missilePrefab.GetComponent<Collider2D>().enabled = true;
                    tl.EndLine();
                    collider1.radius = 0.2f;
                    haveMissile = false;
                    mouseOverMissile.transform.position = transform.position;
                    StartCoroutine(DelayForSpawn());
                }
            }
        }
    }
    private bool UseEnergy(float amount){
        if(currentEnergy.Value >= amount ){
            currentEnergy.Value -= amount;
            return true;
        }
        else{
            return false;
        }
    }
    public void SetEnergyValue(float amount){
        currentEnergy.Value = amount;
    }
    
    private void OnMouseExit() {
        mouseOverMissile.SetActive(false);
    }

    public void OnNotify(int newIndex){
        index = newIndex;
        if(haveMissile){
            missilePrefab.SetActive(false);
            missilePrefab = missiles[index].CreateMissile(transform);
            missilePrefab.GetComponent<Collider2D>().enabled = false;
        }
    }



    private void OnEnable() {
        _inputController.AddObserver(this);
    }

    private void OnDisable() {
        _inputController.RemoveObserver(this);
    }
}
