using System;
using System.Collections.Generic;
using UnityEngine;


public class DamageTypesForNexus : MonoBehaviour 
{
    public Dictionary<string, int> damageDictionary = new Dictionary<string, int>();   
    [SerializeField] private List<string> types;
    [SerializeField] private List<int> damages;
    private static DamageTypesForNexus instance;
    public static DamageTypesForNexus Instance { get {return instance; } }

    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start() {
        for (int i = 0; i < damages.Count; i++)
        {
            damageDictionary.Add(types[i],damages[i]);
        }
    }
}
