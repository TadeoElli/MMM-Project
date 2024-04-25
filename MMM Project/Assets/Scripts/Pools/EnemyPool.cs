using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;

public class EnemyPool : MonoBehaviour
{
    /// <summary>
    /// Esta es una pool de enemigos, guarda en un diccionario, el tipo de enemigo y una lista para guardar varios tipos de ese tipo de enemigo
    /// </summary>
    [SerializeField] private List<EnemyBehaviour> enemyPrefab;        //Lista de tipos de enemigos
    [SerializeField] private int poolSize = 1;          //Cantidad de la pool al inicializar
    [SerializeField] private Dictionary<GameObject, List<GameObject>> enemyDictionary = new Dictionary<GameObject, List<GameObject>>();   //Diccionario para entregar un enemigo y devolver la cantidad generada
    [SerializeField] private ChangeStats killCount, score;
    [SerializeField] private EnemySpawner spawner;
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

        //Creo el diccionario poniendole a cada prefab en la lista una lista de la cantidad de enemigos generados como valor a devolver
        enemyPrefab.ForEach(prefab => enemyDictionary.Add(prefab.gameObject, new List<GameObject>()));
    }

    void Start()
    {
        //Creo todos los objectos para la pool(De cada prefab)
        enemyPrefab.ForEach(prefab => AddEnemyToPool(poolSize, prefab));
    }

    public void AddEnemyToPool(int amount, EnemyBehaviour enemy){       //Le mando cuantos genero y cual misil

        List<GameObject> prefabList = enemyDictionary[enemy.gameObject];    //Guardo la lista de cantidad de enemigos en otra lista
        Enumerable.Range(0, amount).ToList().ForEach(_ => {
            GameObject prefab = Instantiate(enemy.gameObject);
            prefab.GetComponent<EnemyBehaviour>().notifyKillCount = killCount.IncreaseAmount;
            prefab.GetComponent<EnemyBehaviour>().notifyScore += score.IncreaseAmount;
            prefab.GetComponent<EnemyBehaviour>().notifyKillCount += spawner.ReduceEnemiesAlive;
            prefab.SetActive(false);
            prefabList.Add(prefab);
            prefab.transform.parent = transform;

        });
    }

    public GameObject RequestEnemy(EnemyBehaviour enemy){        //Le mando cual necesito

        List<GameObject> prefabList = enemyDictionary[enemy.gameObject];
        var inactivePrefab = prefabList.FirstOrDefault(prefab => !prefab.activeSelf);
        if(inactivePrefab != null){
            inactivePrefab.SetActive(true);
            return inactivePrefab;
        }
        AddEnemyToPool(1,enemy);
        var lastPrefab = prefabList.Last();
        lastPrefab.SetActive(true);
        return lastPrefab;
    }
}
