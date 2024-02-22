using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;        
    [SerializeField] private int poolSize = 5;          //Cantidad de la pool al inicializar
    [SerializeField] private List<GameObject> explosionList;
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
        
    }

    void Start()
    {
        AddExplosionToPool(poolSize);
    }

    public void AddExplosionToPool(int amount){       

        for (int i = 0; i < amount; i++)
        {
            GameObject explosion = Instantiate(explosionPrefab);
            explosion.SetActive(false);
            explosionList.Add(explosion);
            explosion.transform.parent = transform;
        }
    }

    public GameObject RequestExplosion(){    
        for (int i = 0; i < explosionList.Count; i++)
        {
            if(!explosionList[i].activeSelf){
                explosionList[i].SetActive(true);
                return explosionList[i];
            }
        }
        AddExplosionToPool(1);
        explosionList[explosionList.Count - 1].SetActive(true);
        return explosionList[explosionList.Count - 1];
    }
}
