using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MissilePool : MonoBehaviour
{
    /// <summary>
    /// Esta es una pool de misiles, guarda en un diccionario, el tipo de misil y una lista para guardar varios tipos de ese tipo de misil
    /// </summary>
    [SerializeField] private List<MissileBehaviour> missilePrefab;        //Lista de misiles
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

        //Creo el diccionario poniendole a cada prefab en la lista una lista de la cantidad de misiles generados como valor a devolver
        missilePrefab.ForEach(prefab => missileDictionary.Add(prefab.gameObject, new List<GameObject>()));
    }

    void Start()
    {
        //Creo todos los objectos para la pool(De cada prefab)
        missilePrefab.ForEach(prefab => AddMissilesToPool(poolSize, prefab));
    }

    public void AddMissilesToPool(int amount, MissileBehaviour prefab){       //Le mando cuantos genero y cual misil

        List<GameObject> prefabList = missileDictionary[prefab.gameObject];    //Guardo la lista de cantidad de misiles en otra lista
        Enumerable.Range(0, amount).ToList().ForEach(_ => {
            GameObject missile = Instantiate(prefab.gameObject);
            missile.SetActive(false);
            prefabList.Add(missile);
            missile.transform.parent = transform;

        });
    }

    public GameObject RequestMissile(MissileBehaviour prefab){        //Le mando cual necesito

        List<GameObject> prefabList = missileDictionary[prefab.gameObject];
        bool hasInactivePrefab = prefabList.Any(prefab => !prefab.activeSelf);
        if(!hasInactivePrefab) AddMissilesToPool(1,prefab);
        GameObject prefabToReturn = hasInactivePrefab ? prefabList.FirstOrDefault(x => !x.activeSelf) : prefabList.LastOrDefault();
        prefabToReturn.SetActive(true);
        return prefabToReturn;
    }
}
