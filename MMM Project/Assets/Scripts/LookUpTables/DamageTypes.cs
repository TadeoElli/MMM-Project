using System;
using System.Collections.Generic;
using UnityEngine;


public class DamageTypes : MonoBehaviour 
{
    [Header("Damage of explosions of missiles")]
    public Dictionary<int, int> explosionDictionary = new Dictionary<int, int>(); 
    [SerializeField] private List<int> id;
    [SerializeField] private List<int> damagesOfExplosions;

    [Header("Damage of collision for missiles")]
    public Dictionary<int, int> collisionMissilesDictionary = new Dictionary<int, int>();   
    [SerializeField] private List<int> types;
    [SerializeField] private List<int> damagesOfCollisionsForMissiles;

    [Header("Damage of collision for enemies")]
    public Dictionary<int, int> collisionEnemiesDictionary = new Dictionary<int, int>();   
    [SerializeField] private List<int> damagesOfCollisionsForEnemies;
    private static DamageTypes instance;
    public static DamageTypes Instance { get {return instance; } }

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
        for (int i = 0; i < damagesOfExplosions.Count; i++)
        {
            explosionDictionary.Add(i,damagesOfExplosions[i]);
        }
        for (int i = 0; i < damagesOfCollisionsForMissiles.Count; i++)
        {
            collisionMissilesDictionary.Add(types[i],damagesOfCollisionsForMissiles[i]);
        }
        for (int i = 0; i < damagesOfCollisionsForEnemies.Count; i++)
        {
            collisionMissilesDictionary.Add(types[i],damagesOfCollisionsForEnemies[i]);
        }
    }
}
