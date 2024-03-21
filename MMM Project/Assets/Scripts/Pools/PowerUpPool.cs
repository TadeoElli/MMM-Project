using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> powerUpPrefab;        //Lista de misiles
    [SerializeField] private int poolSize = 1;          //Cantidad de la pool al inicializar
    [SerializeField] private Dictionary<GameObject, List<GameObject>> powerUpDictionary = new Dictionary<GameObject, List<GameObject>>();   //Diccionario para entregar un misil y devolver la cantidad generada
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

        foreach (GameObject prefabs in powerUpPrefab)       //Creo el diccionario poniendole a cada prefab en la lista una lista de la cantidad de misiles generados como valor a devolver
        {
            powerUpDictionary.Add(prefabs, new List<GameObject>());
        }
    }

    void Start()
    {
        for (int i = 0; i < powerUpPrefab.Count; i++)       //Creo todos los objectos para la pool(De cada prefab)
        {
            AddPowerUpToPool(poolSize, powerUpPrefab[i]);
        }
    }

    public void AddPowerUpToPool(int amount, GameObject prefab){       //Le mando cuantos genero y cual misil

        List<GameObject> prefabList = powerUpDictionary[prefab];    //Guardo la lista de cantidad de misiles en otra lista
        for (int i = 0; i < amount; i++)
        {
            GameObject powerUp = Instantiate(prefab);
            powerUp.SetActive(false);
            prefabList.Add(powerUp);
            powerUp.transform.SetParent(transform, false);
        }
    }

    public GameObject RequestPowerUp(GameObject prefab){        //Le mando cual necesito

        List<GameObject> prefabList = powerUpDictionary[prefab];
        for (int i = 0; i < prefabList.Count; i++)
        {
            if(!prefabList[i].activeSelf){
                prefabList[i].SetActive(true);
                return prefabList[i];
            }
        }
        AddPowerUpToPool(1,prefab);
        prefabList[prefabList.Count - 1].SetActive(true);
        return prefabList[prefabList.Count - 1];
    }
}