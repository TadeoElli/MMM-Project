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
        //IA2-LINQ
        //Creo diccionarios de los objetos(explosiones o layers) y su respectivo daño a realizar, utilizando zip para combinar la lista de objetos
        //con la de daños y transformandolo en un diccionario
        explosionDictionary = explosionList.Zip(damagesOfExplosions, (explosion, damage) => new { explosion, damage }).ToDictionary(pair => pair.explosion, pair => pair.damage);

        collisionMissilesDictionary = types.Zip(damagesOfCollisionsForMissiles, (type, damage) => new { type, damage }).ToDictionary(pair => pair.type, pair => pair.damage);

        collisionEnemiesDictionary = types.Zip(damagesOfCollisionsForEnemies, (type, damage) => new { type, damage }).ToDictionary(pair => pair.type, pair => pair.damage);
    }
}
