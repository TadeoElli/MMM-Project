using System;
using System.Collections.Generic;
using UnityEngine;


public class DamageTypesForNexus : MonoBehaviour 
{
    [Header("Damage of explosions of missiles")]
    public Dictionary<MissileStrategy, int> missilesDictionary = new Dictionary<MissileStrategy, int>(); 
    [SerializeField] private List<MissileStrategy> types;
    [SerializeField] private List<int> damages;
    //public Dictionary<MissileStrategy, int> missilesDictionary = new Dictionary<MissileStrategy, int>();  
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
            missilesDictionary.Add(types[i],damages[i]);
        }
    }
}
