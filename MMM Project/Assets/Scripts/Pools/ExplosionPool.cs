using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> explosionPrefab;        //Lista de misiles      
    [SerializeField] private int poolSize = 5;          //Cantidad de la pool al inicializar
    [SerializeField] private Dictionary<GameObject, List<GameObject>> explosionDictionary = new Dictionary<GameObject, List<GameObject>>();   //Diccionario para entregar un misil y devolver la cantidad generada

    private static ExplosionPool instance;
    public static ExplosionPool Instance { get {return instance; } }

    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        foreach (GameObject prefabs in explosionPrefab)       //Creo el diccionario poniendole a cada prefab en la lista una lista de la cantidad de misiles generados como valor a devolver
        {
            explosionDictionary.Add(prefabs, new List<GameObject>());
        }
    }

    void Start()
    {
        for (int i = 0; i < explosionPrefab.Count; i++)       //Creo todos los objectos para la pool(De cada prefab)
        {
            AddExplosionToPool(poolSize, explosionPrefab[i]);
        }
    }

    public void AddExplosionToPool(int amount, GameObject prefab){       

        List<GameObject> prefabList = explosionDictionary[prefab];    //Guardo la lista de cantidad de misiles en otra lista
        for (int i = 0; i < amount; i++)
        {
            GameObject explosion = Instantiate(prefab);
            explosion.SetActive(false);
            prefabList.Add(explosion);
            explosion.transform.parent = transform;
        }
    }

    public GameObject RequestExplosion(GameObject prefab){    
        List<GameObject> prefabList = explosionDictionary[prefab];
        for (int i = 0; i < prefabList.Count; i++)
        {
            if(!prefabList[i].activeSelf){
                prefabList[i].SetActive(true);
                return prefabList[i];
            }
        }
        AddExplosionToPool(1,prefab);
        prefabList[prefabList.Count - 1].SetActive(true);
        return prefabList[prefabList.Count - 1];
    }
}
