using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TowerController : MonoBehaviour
{
    /// <summary>
    /// Esta clase se encarga de administrar todas las torres, spawnearlas y administrar sus cooldowns
    /// </summary>
    // Start is called before the first frame update
    public Observer<int> currentIndex = new Observer<int>(0);   //el indice de que torreta esta activa
    Camera cam;
    [SerializeField] private bool hasTower = false;     //Si tiene una torre activa en el momento
    [SerializeField] private float distanceFromNexus;   //La distancia del nexo en donde se puede empezar a colocar torres
    private GameObject nexus;   //El nexo para calcular la distancia

    [SerializeField] private TowerStrategy [] towers;   //Todos los strategys de las torres
    
    [SerializeField] private List<float> cooldowns; //La lista de cooldowns
    [SerializeField] private List<float> currentCd; //la lista de sus temporizadores
    [SerializeField] private List<bool> isReady;    //La lista de flags de si ya estan disponibles
    [SerializeField] private List<UnityEvent> changeTowersHud;

    private void Awake() {
        cam = Camera.main;
        nexus = FindObjectOfType<Nexus>().gameObject;
    }
    void Start()    //Creo Las listas de cooldowns, flags y temporizadores, dejando la 0 vacia como default
    {
        for (int i = 0; i < towers.Length; i++)
        {
            if(i == 0){
                cooldowns.Add(0);
                currentCd.Add(0);
                isReady.Add(false);
            }
            else{
                cooldowns.Add(towers[i].cooldown);
                currentCd.Add(towers[i].cooldown);
                isReady.Add(true);
            }
        }
    }

    
    void Update()   //Si uno de las torres de la lista no esta listo, aumento su temporizador hasta que supere su cooldown y ahi lo activo
    {
        for (int i = 1; i < towers.Length; i++)
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

    public void SetTowerIndex(int newIndex){    //Cambia el indice de las torres, si el nuevo indice la torre todavia no esta lista, vuelve a 0
        if(!isReady[newIndex]){
            currentIndex.Value = 0;
            hasTower = false;
        }
        else{
            currentIndex.Value = newIndex;
            hasTower = true;
            CursorController.Instance.SetCursor(towers[newIndex].sprite, towers[newIndex].material, towers[newIndex].scale);
        }
    }

    //Si tiene una torre activa, chequea si la distancia hacia el nexo es lo suficiente para crearla
    public void ActivateTower(){ 
        if(hasTower){
            if(CheckDistanceFromNexus()){
                CreateTower();
                currentIndex.Value = 0;
            }
        }
    }
    //Si hay una torre activa, la desactiva y vuelve el indice a 0
    public void DesactivateTower(){
        if(hasTower){
            hasTower = false;
            currentIndex.Value = 0;
            CursorController.Instance.RestoreCursor();
        }
    }
    //Chequea la distancia del mouse al nexo y si es menor a la distancia minima devuelve un false, si no un true
    private bool CheckDistanceFromNexus(){
        Vector2 currentPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        if(Vector2.Distance(currentPosition, nexus.transform.position) > distanceFromNexus){
            return true;
        }
        else{
            return false;
        }
    }
    //Llama a la funcion CreateTower de la torre, pone que la torre no esta lista y resetea su cooldown
    private void CreateTower(){
        towers[currentIndex.Value].CreateTower(cam.ScreenToWorldPoint(Input.mousePosition));
        hasTower = false;
        isReady[currentIndex.Value] = false;
        changeTowersHud[currentIndex.Value]?.Invoke();
        currentCd[currentIndex.Value] = 0;
        CursorController.Instance.RestoreCursor();
    }
}
