using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Linq;

public class ExplosionPool : MonoBehaviour
{
     /// <summary>
    /// Esta es una pool de explosiones, guarda en un diccionario, el tipo de explosion y una lista para guardar varios tipos de ese tipo de explosion
    /// </summary>
    [SerializeField] private List<Explosion> explosionPrefab;        //Lista de explosion      
    [SerializeField] private int poolSize = 5;          //Cantidad de la pool al inicializar
    [SerializeField] private Dictionary<GameObject, List<GameObject>> explosionDictionary = new Dictionary<GameObject, List<GameObject>>();   //Diccionario para entregar un misil y devolver la cantidad generada

    private static ExplosionPool instance;
    public static ExplosionPool Instance { get {return instance; } }
    Stopwatch stopwatch = new Stopwatch();

    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        //Creo el diccionario poniendole a cada prefab en la lista una lista de la cantidad de explosion generados como valor a devolver
        explosionPrefab.ForEach(prefab => explosionDictionary.Add(prefab.gameObject, new List<GameObject>()));
    }

    void Start()
    {
        //Creo todos los objectos para la pool(De cada prefab)
        //explosionPrefab.ForEach(prefab => AddExplosionToPool(poolSize, prefab));
        //IA2-P4”.
        StartCoroutine(AddExplosionToPoolCoroutine());
    }
    private IEnumerator AddExplosionToPoolCoroutine(){
        stopwatch.Start();
        foreach (var prefab in explosionPrefab)
        {
            yield return StartCoroutine(AddExplosionToPool(poolSize, prefab));
        }
    }

    public IEnumerator AddExplosionToPool(int amount, Explosion prefab){       

        List<GameObject> prefabList = explosionDictionary[prefab.gameObject];    //Guardo la lista de cantidad de explosion en otra lista
        for (int i = 0; i < amount; i++)
        {
            CreateExplosion(prefabList, prefab);
            // Espera un frame antes de continuar con la siguiente instancia
            if(stopwatch.ElapsedMilliseconds > 1f / 60f ){
                yield return new WaitForEndOfFrame();
                stopwatch.Restart();
                //UnityEngine.Debug.Log("Spawnie explosions en un frame");
            }
        }
    }
    private void CreateExplosion(List<GameObject> prefabList, Explosion prefab){
        GameObject explosion = Instantiate(prefab.gameObject);
        explosion.SetActive(false);
        prefabList.Add(explosion);
        explosion.transform.parent = transform;
    }

    public GameObject RequestExplosion(Explosion prefab){  
        //IA2-LINQ
        //Cuando se pide el tipo de objeto, se pregunta si ya hay uno en la lista que no este activo y segun eso
        //Se devuelve el primero de la lista que no este activo o se crea uno nuevo y se devuelve  el ultimo de la lista  
        List<GameObject> prefabList = explosionDictionary[prefab.gameObject];
        bool hasInactivePrefab = prefabList.Any(prefab => !prefab.activeSelf);
        if(!hasInactivePrefab) CreateExplosion(prefabList,prefab);
        GameObject prefabToReturn = hasInactivePrefab ? prefabList.FirstOrDefault(x => !x.activeSelf) : prefabList.LastOrDefault();
        prefabToReturn.SetActive(true);
        return prefabToReturn;
    }
}
