using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePool : MonoBehaviour
{
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private int poolSize = 5;
    [SerializeField] private List<GameObject> missilelist;

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
    }

    void Start()
    {
        AddMissilesToPool(poolSize);
    }

    public void AddMissilesToPool(int amount){

        for (int i = 0; i < amount; i++)
        {
            GameObject missile = Instantiate(missilePrefab);
            missile.SetActive(false);
            missilelist.Add(missile);
            missile.transform.parent = transform;
        }
    }

    public GameObject RequestMissile(){
        for (int i = 0; i < missilelist.Count; i++)
        {
            if(!missilelist[i].activeSelf){
                missilelist[i].SetActive(true);
                return missilelist[i];
            }
        }
        AddMissilesToPool(1);
        missilelist[missilelist.Count - 1].SetActive(true);
        return missilelist[missilelist.Count - 1];
    }
}
