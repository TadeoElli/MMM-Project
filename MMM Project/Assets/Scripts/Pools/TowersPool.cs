using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersPool : MonoBehaviour
{
    [SerializeField] private List<TowerBehaviour> towerPrefab;        //Lista de misiles
    [SerializeField] private int poolSize = 1;          //Cantidad de la pool al inicializar
    [SerializeField] private Dictionary<GameObject, List<GameObject>> towerDictionary = new Dictionary<GameObject, List<GameObject>>();   //Diccionario para entregar un misil y devolver la cantidad generada
    private static TowersPool instance;
    public static TowersPool Instance { get {return instance; } }

    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (TowerBehaviour prefabs in towerPrefab)       //Creo el diccionario poniendole a cada prefab en la lista una lista de la cantidad de misiles generados como valor a devolver
        {
            towerDictionary.Add(prefabs.gameObject, new List<GameObject>());
        }
    }

    void Start()
    {
        for (int i = 0; i < towerPrefab.Count; i++)       //Creo todos los objectos para la pool(De cada prefab)
        {
            AddTowersToPool(poolSize, towerPrefab[i]);
        }
    }

    public void AddTowersToPool(int amount, TowerBehaviour prefab){       //Le mando cuantos genero y cual misil

        List<GameObject> prefabList = towerDictionary[prefab.gameObject];    //Guardo la lista de cantidad de misiles en otra lista
        for (int i = 0; i < amount; i++)
        {
            GameObject tower = Instantiate(prefab.gameObject);
            tower.SetActive(false);
            prefabList.Add(tower);
            tower.transform.parent = transform;
        }
    }

    public GameObject RequestTower(TowerBehaviour prefab){        //Le mando cual necesito

        List<GameObject> prefabList = towerDictionary[prefab.gameObject];
        for (int i = 0; i < prefabList.Count; i++)
        {
            if(!prefabList[i].activeSelf){
                prefabList[i].SetActive(true);
                return prefabList[i];
            }
        }
        AddTowersToPool(1,prefab);
        prefabList[prefabList.Count - 1].SetActive(true);
        return prefabList[prefabList.Count - 1];
    }
}
