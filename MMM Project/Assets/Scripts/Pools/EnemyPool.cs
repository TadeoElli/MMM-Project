using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EnemyPool : MonoBehaviour
{
    /// <summary>
    /// Esta es una pool de enemigos, guarda en un diccionario, el tipo de enemigo y una lista para guardar varios tipos de ese tipo de enemigo
    /// </summary>
    [SerializeField] private List<EnemyBehaviour> enemyPrefab;        //Lista de tipos de enemigos
    [SerializeField] private int poolSize = 1;          //Cantidad de la pool al inicializar
    [SerializeField] private Dictionary<GameObject, List<GameObject>> enemyDictionary = new Dictionary<GameObject, List<GameObject>>();   //Diccionario para entregar un enemigo y devolver la cantidad generada
    [SerializeField] private ChangeStats killCount;
    //public event OnEnemyDeath _OnEnemyDeath;

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

        foreach (EnemyBehaviour prefabs in enemyPrefab)       //Creo el diccionario poniendole a cada prefab en la lista una lista de la cantidad de enemigos generados como valor a devolver
        {
            enemyDictionary.Add(prefabs.gameObject, new List<GameObject>());
        }
    }

    void Start()
    {
        for (int i = 0; i < enemyPrefab.Count; i++)       //Creo todos los objectos para la pool(De cada prefab)
        {
            AddEnemyToPool(poolSize, enemyPrefab[i]);
        }
    }

    public void AddEnemyToPool(int amount, EnemyBehaviour enemy){       //Le mando cuantos genero y cual misil

        List<GameObject> prefabList = enemyDictionary[enemy.gameObject];    //Guardo la lista de cantidad de enemigos en otra lista
        for (int i = 0; i < amount; i++)
        {
            GameObject prefab = Instantiate(enemy.gameObject);
            prefab.GetComponent<EnemyBehaviour>()._OnEnemyDeath = killCount.IncreaseAmount;
            prefab.SetActive(false);
            prefabList.Add(prefab);
            prefab.transform.parent = transform;
        }
    }

    public GameObject RequestEnemy(EnemyBehaviour enemy){        //Le mando cual necesito

        List<GameObject> prefabList = enemyDictionary[enemy.gameObject];
        for (int i = 0; i < prefabList.Count; i++)
        {
            if(!prefabList[i].activeSelf){
                prefabList[i].SetActive(true);
                return prefabList[i];
            }
        }
        AddEnemyToPool(1,enemy);
        prefabList[prefabList.Count - 1].SetActive(true);
        return prefabList[prefabList.Count - 1];
    }
}
