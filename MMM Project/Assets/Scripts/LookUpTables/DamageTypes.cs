using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
    

public class DamageTypes : MonoBehaviour 
{   
    ///LookUpTable <summary>
        /// Esta clase se encarga de crear diccionarios con distintos tipos de objetos y sus respectivos daños para hacer un LookUpTable de daños
        /// Se colocan todos en esta clase para asi manejarlos desde el inspector de un solo objeto
        /// </summary>    
    [Header("Damage of explosions of missiles")]
    [SerializeField] private List<ExplosionsTypes> explosionList = new List<ExplosionsTypes>(); //Lista de los tipos de explosiones
    [SerializeField] private List<int> damagesOfExplosions; //Lista de daños de explosiones
    public Dictionary<ExplosionsTypes, int> explosionDictionary = new Dictionary<ExplosionsTypes, int>(); 

    [SerializeField] private List<int> types;   //Lista de tipos de collisiones (layers)
    [Header("Damage of collision for missiles")]
    [SerializeField] private List<int> damagesOfCollisionsForMissiles;  //Lista de daños por colisiones para los misiles
    public Dictionary<int, int> collisionMissilesDictionary = new Dictionary<int, int>();   

    [Header("Damage of collision for enemies")]
    [SerializeField] private List<int> damagesOfCollisionsForEnemies;   //Lista de daños por colisiones para los enemigos
    public Dictionary<int, int> collisionEnemiesDictionary = new Dictionary<int, int>();   
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
    
    private void Start() {  //Creo los diccionarios
        explosionList = Enum.GetValues(typeof(ExplosionsTypes)).Cast<ExplosionsTypes>().ToList();
        for (int i = 0; i < damagesOfExplosions.Count; i++)
        {
            explosionDictionary.Add(explosionList[i],damagesOfExplosions[i]);
        }
        for (int i = 0; i < damagesOfCollisionsForMissiles.Count; i++)
        {
            collisionMissilesDictionary.Add(types[i],damagesOfCollisionsForMissiles[i]);
        }
        for (int i = 0; i < damagesOfCollisionsForEnemies.Count; i++)
        {
            collisionEnemiesDictionary.Add(types[i],damagesOfCollisionsForEnemies[i]);
        }
    }
}
