using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefab;        //Lista de misiles
    [SerializeField] private int poolSize = 1;          //Cantidad de la pool al inicializar
    [SerializeField] private Dictionary<GameObject, List<GameObject>> enemyDictionary = new Dictionary<GameObject, List<GameObject>>();   //Diccionario para entregar un misil y devolver la cantidad generada
    private static EnemyPool instance;
    public static EnemyPool Instance { get {return instance; } }

    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (GameObject prefabs in enemyPrefab)       //Creo el diccionario poniendole a cada prefab en la lista una lista de la cantidad de misiles generados como valor a devolver
        {
            enemyDictionary.Add(prefabs, new List<GameObject>());
        }
    }

    void Start()
    {
        for (int i = 0; i < enemyPrefab.Count; i++)       //Creo todos los objectos para la pool(De cada prefab)
        {
            AddEnemyToPool(poolSize, enemyPrefab[i]);
        }
    }

    public void AddEnemyToPool(int amount, GameObject prefab){       //Le mando cuantos genero y cual misil

        List<GameObject> prefabList = enemyDictionary[prefab];    //Guardo la lista de cantidad de misiles en otra lista
        for (int i = 0; i < amount; i++)
        {
            GameObject tower = Instantiate(prefab);
            tower.SetActive(false);
            prefabList.Add(tower);
            tower.transform.parent = transform;
        }
    }

    public GameObject RequestEnemy(GameObject prefab){        //Le mando cual necesito

        List<GameObject> prefabList = enemyDictionary[prefab];
        for (int i = 0; i < prefabList.Count; i++)
        {
            if(!prefabList[i].activeSelf){
                prefabList[i].SetActive(true);
                return prefabList[i];
            }
        }
        AddEnemyToPool(1,prefab);
        prefabList[prefabList.Count - 1].SetActive(true);
        return prefabList[prefabList.Count - 1];
    }
}
