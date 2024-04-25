using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        explosionPrefab.ForEach(prefab => AddExplosionToPool(poolSize, prefab));
    }

    public void AddExplosionToPool(int amount, Explosion prefab){       

        List<GameObject> prefabList = explosionDictionary[prefab.gameObject];    //Guardo la lista de cantidad de explosion en otra lista
        Enumerable.Range(0, amount).ToList().ForEach(_ => {
            GameObject explosion = Instantiate(prefab.gameObject);
            explosion.SetActive(false);
            prefabList.Add(explosion);
            explosion.transform.parent = transform;

        });
    }

    public GameObject RequestExplosion(Explosion prefab){    
        List<GameObject> prefabList = explosionDictionary[prefab.gameObject];
        var inactivePrefab = prefabList.FirstOrDefault(prefab => !prefab.activeSelf);
        if(inactivePrefab != null){
            inactivePrefab.SetActive(true);
            return inactivePrefab;
        }
        AddExplosionToPool(1,prefab);
        var lastPrefab = prefabList.Last();
        lastPrefab.SetActive(true);
        return lastPrefab;
    }
}
