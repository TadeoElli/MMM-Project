using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Diagnostics;

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
    Stopwatch stopwatch = new Stopwatch();
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
        //missilePrefab.ForEach(prefab => AddMissilesToPool(poolSize, prefab));
        //IA2-P4”.
        StartCoroutine(AddMissileToPoolCoroutine());
    }

    private IEnumerator AddMissileToPoolCoroutine(){
        stopwatch.Start();
        foreach (var prefab in missilePrefab)
        {
            yield return StartCoroutine(AddMissilesToPool(poolSize, prefab));
        }
    }
    public IEnumerator AddMissilesToPool(int amount, MissileBehaviour prefab){       //Le mando cuantos genero y cual misil

        List<GameObject> prefabList = missileDictionary[prefab.gameObject];    //Guardo la lista de cantidad de misiles en otra lista
        for (int i = 0; i < amount; i++)
        {
            CreateMissile(prefabList, prefab);
            // Espera un frame antes de continuar con la siguiente instancia
            if(stopwatch.ElapsedMilliseconds > 1f / 60f ){
                yield return new WaitForEndOfFrame();
                stopwatch.Restart();
                //UnityEngine.Debug.Log("Spawnie misiles en un frame");
            }
        }
    }
    private void CreateMissile(List<GameObject> prefabList, MissileBehaviour prefab){
        GameObject missile = Instantiate(prefab.gameObject);
        missile.SetActive(false);
        prefabList.Add(missile);
        missile.transform.parent = transform;
    }

    public GameObject RequestMissile(MissileBehaviour prefab){        //Le mando cual necesito
        //IA2-LINQ
        //Cuando se pide el tipo de objeto, se pregunta si ya hay uno en la lista que no este activo y segun eso
        //Se devuelve el primero de la lista que no este activo o se crea uno nuevo y se devuelve  el ultimo de la lista
        List<GameObject> prefabList = missileDictionary[prefab.gameObject];
        bool hasInactivePrefab = prefabList.Any(prefab => !prefab.activeSelf);
        if(!hasInactivePrefab) CreateMissile(prefabList,prefab);
        GameObject prefabToReturn = hasInactivePrefab ? prefabList.FirstOrDefault(x => !x.activeSelf) : prefabList.LastOrDefault();
        prefabToReturn.SetActive(true);
        return prefabToReturn;
    }
}
