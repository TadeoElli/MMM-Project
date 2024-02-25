using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePool : MonoBehaviour
{
    [SerializeField] private List<GameObject> missilePrefab;        //Lista de misiles
    [SerializeField] private int poolSize = 5;          //Cantidad de la pool al inicializar
    [SerializeField] private Dictionary<GameObject, List<GameObject>> missileDictionary = new Dictionary<GameObject, List<GameObject>>();   //Diccionario para entregar un misil y devolver la cantidad generada
    private static MissilePool instance;
    public static MissilePool Instance { get {return instance; } }

    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (GameObject prefabs in missilePrefab)       //Creo el diccionario poniendole a cada prefab en la lista una lista de la cantidad de misiles generados como valor a devolver
        {
            missileDictionary.Add(prefabs, new List<GameObject>());
        }
    }

    void Start()
    {
        for (int i = 0; i < missilePrefab.Count; i++)       //Creo todos los objectos para la pool(De cada prefab)
        {
            AddMissilesToPool(poolSize, missilePrefab[i]);
        }
    }

    public void AddMissilesToPool(int amount, GameObject prefab){       //Le mando cuantos genero y cual misil

        List<GameObject> prefabList = missileDictionary[prefab];    //Guardo la lista de cantidad de misiles en otra lista
        for (int i = 0; i < amount; i++)
        {
            GameObject missile = Instantiate(prefab);
            missile.SetActive(false);
            prefabList.Add(missile);
            missile.transform.parent = transform;
        }
    }

    public GameObject RequestMissile(GameObject prefab){        //Le mando cual necesito

        List<GameObject> prefabList = missileDictionary[prefab];
        for (int i = 0; i < prefabList.Count; i++)
        {
            if(!prefabList[i].activeSelf){
                prefabList[i].SetActive(true);
                return prefabList[i];
            }
        }
        AddMissilesToPool(1,prefab);
        prefabList[prefabList.Count - 1].SetActive(true);
        return prefabList[prefabList.Count - 1];
    }
}
