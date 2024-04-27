using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

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
        cooldowns = towers.SetCooldownsValue(x => x.cooldown - 0).ToList();
        //cooldowns.Add(0);
        currentCd = towers.SetCooldownsValue(x => x.cooldown - 0).ToList();
        //currentCd = cooldowns;
        isReady = new List<bool> { false }
        .Concat(towers.Skip(1).Select(tower => true))
        .ToList();
        
    }

    
    void Update()   //Si uno de las torres de la lista no esta listo, aumento su temporizador hasta que supere su cooldown y ahi lo activo
    {
        var notReadyIndices = Enumerable.Range(1, towers.Length - 1)
        .Where(i => !isReady[i])
        .ToList();

        foreach (var index in notReadyIndices)
        {
            currentCd[index] = Mathf.Clamp(currentCd[index] + Time.deltaTime, 0, cooldowns[index]);
            if (currentCd[index] >= cooldowns[index])
            {
                isReady[index] = true;
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
        if(hasTower && CheckDistance()){
            CreateTower();
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
    private bool CheckDistance(){
        // Check distance to nexus
        Vector2 currentPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        float distanceToNexus = Vector2.Distance(currentPosition, nexus.transform.position);
        bool isValidPosition = distanceToNexus > distanceFromNexus && currentPosition.y > -3f && currentPosition.y < 4.25f;

        if (!isValidPosition)
        {
            AudioManager.Instance.PlaySoundEffect(towers[currentIndex.Value].invalidEffect);
        }

        return isValidPosition;
    }
    //Llama a la funcion CreateTower de la torre, pone que la torre no esta lista y resetea su cooldown
    private void CreateTower(){
        towers[currentIndex.Value].CreateTower(cam.ScreenToWorldPoint(Input.mousePosition));
        hasTower = false;
        isReady[currentIndex.Value] = false;
        changeTowersHud[currentIndex.Value]?.Invoke();
        currentCd[currentIndex.Value] = 0;
        CursorController.Instance.RestoreCursor();
        currentIndex.Value = 0;
    }
}
