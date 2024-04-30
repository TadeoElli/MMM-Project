using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PowerUpPool : MonoBehaviour
{
    /// <summary>
    /// Esta es una pool de PowerUp, guarda en un diccionario, el tipo de PowerUp y una lista para guardar varios tipos de ese tipo de PowerUps
    /// </summary>
    [SerializeField] private List<PowerUp> powerUpPrefab;        //Lista de PowerUps
    [SerializeField] private int poolSize = 1;          //Cantidad de la pool al inicializar
    [SerializeField] private Dictionary<GameObject, List<GameObject>> powerUpDictionary = new Dictionary<GameObject, List<GameObject>>();   //Diccionario para entregar un PowerUps y devolver la cantidad generada
    private static PowerUpPool instance;
    public static PowerUpPool Instance { get {return instance; } }

    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //Creo el diccionario poniendole a cada prefab en la lista una lista de la cantidad de PowerUps generados como valor a devolver
        powerUpPrefab.ForEach(prefab => powerUpDictionary.Add(prefab.gameObject, new List<GameObject>()));
    }

    void Start()
    {
        //Creo todos los objectos para la pool(De cada prefab)
        powerUpPrefab.ForEach(prefab => AddPowerUpToPool(poolSize, prefab));
    }

    public void AddPowerUpToPool(int amount, PowerUp prefab){       //Le mando cuantos genero y cual PowerUps

        List<GameObject> prefabList = powerUpDictionary[prefab.gameObject];    //Guardo la lista de cantidad de PowerUps en otra lista
        Enumerable.Range(0, amount).ToList().ForEach(_ => {
            GameObject powerUp = Instantiate(prefab.gameObject);
            powerUp.SetActive(false);
            prefabList.Add(powerUp);
            powerUp.transform.SetParent(transform);

        });
    }

    public GameObject RequestPowerUp(PowerUp prefab){        //Le mando cual necesito
        //IA2-LINQ
        //Cuando se pide el tipo de objeto, se pregunta si ya hay uno en la lista que no este activo y segun eso
        //Se devuelve el primero de la lista que no este activo o se crea uno nuevo y se devuelve  el ultimo de la lista
        List<GameObject> prefabList = powerUpDictionary[prefab.gameObject];
        bool hasInactivePrefab = prefabList.Any(prefab => !prefab.activeSelf);
        if(!hasInactivePrefab) AddPowerUpToPool(1,prefab);
        GameObject prefabToReturn = hasInactivePrefab ? prefabList.FirstOrDefault(x => !x.activeSelf) : prefabList.LastOrDefault();
        prefabToReturn.SetActive(true);
        return prefabToReturn;
    }
}