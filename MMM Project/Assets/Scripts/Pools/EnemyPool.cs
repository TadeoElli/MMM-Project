using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Diagnostics;
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
    Stopwatch stopwatch = new Stopwatch();
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
        //enemyPrefab.ForEach(prefab => AddEnemyToPool(poolSize, prefab));
        //IA2-P4”.
        StartCoroutine(AddEnemiesToPoolCoroutine());
    }
    private IEnumerator AddEnemiesToPoolCoroutine(){
        stopwatch.Start();
        foreach (var prefab in enemyPrefab)
        {
            yield return StartCoroutine(AddEnemyToPool(poolSize, prefab));
        }
    }
    private IEnumerator AddEnemyToPool(int amount, EnemyBehaviour enemy){       //Le mando cuantos genero y cual misil

        List<GameObject> prefabList = enemyDictionary[enemy.gameObject];    //Guardo la lista de cantidad de enemigos en otra lista
        for (int i = 0; i < amount; i++)
        {
            CreateEnemy(prefabList, enemy);
            // Espera un frame antes de continuar con la siguiente instancia
            if(stopwatch.ElapsedMilliseconds > 1f / 60f ){
                yield return new WaitForEndOfFrame();
                stopwatch.Restart();
                //UnityEngine.Debug.Log("Spawnie enemies en un frame");
            }
        }
    }
    private void CreateEnemy(List<GameObject> prefabList, EnemyBehaviour enemy){
        GameObject prefab = Instantiate(enemy.gameObject);
        prefab.GetComponent<EnemyBehaviour>().notifyKillCount = killCount.IncreaseAmount;
        prefab.GetComponent<EnemyBehaviour>().notifyScore += score.IncreaseAmount;
        prefab.GetComponent<EnemyBehaviour>().notifyKillCount += spawner.ReduceEnemiesAlive;
        prefab.SetActive(false);
        prefabList.Add(prefab);
        prefab.transform.parent = SpatialGrid.Instance.transform;
    }

    public GameObject RequestEnemy(EnemyBehaviour enemy){        //Le mando cual necesito
        //IA2-LINQ
        //Cuando se pide el tipo de objeto, se pregunta si ya hay uno en la lista que no este activo y segun eso
        //Se devuelve el primero de la lista que no este activo o se crea uno nuevo y se devuelve  el ultimo de la lista
        List<GameObject> prefabList = enemyDictionary[enemy.gameObject];
        bool hasInactivePrefab = prefabList.Any(prefab => !prefab.activeSelf);
        if(!hasInactivePrefab) CreateEnemy(prefabList,enemy);
        GameObject prefab = hasInactivePrefab ? prefabList.FirstOrDefault(x => !x.activeSelf) : prefabList.Last();
        if (prefab != null)
        {
            prefab.SetActive(true);
        }
        return prefab;


    }
}
