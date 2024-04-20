using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Missile", menuName = "ScriptableObject/Missiles/Divide", order = 5)]
public class DivideMissileStrategy : MissileStrategy
///Este tipo de misil al chocar con un enemigo se subdivide en nuevos tipos de misiles con direcciones aleatorias
{
    [Header("Special Properties")]
    [SerializeField] MissileBehaviour subMissiles;  //Que tipo de misil van a ser los que spawnea
    [SerializeField] private float force;   //Con que fuerza van a ser disparados
    [SerializeField] private int cantOfSubmissiles;     //La cantidad de misiles que spawnea
    

    //El comportamiento de colision
    public override int CollisionBehaviour(GameObject other, GameObject prefab){
        MissileBehaviour missileBehaviour = prefab.GetComponent<MissileBehaviour>();
            
        int layer = other.layer;
        int damage;
        switch (layer)
        {
            case 7:
                AudioManager.Instance.PlaySoundEffect(bounceEffect);
                return 0;
            case 8:
            case 9:
            case 10:
            case 11:    //Recibe 2 veces daño por el "oneChance" que hace que despues de que se quede  con 0 de vida le 
                //permite aguantar un rebote mas
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                missileBehaviour.TakeDamage(10);
                missileBehaviour.TakeDamage(10);
                for (int i = 0; i < cantOfSubmissiles; i++)
                {
                    CreateSubmissile(prefab.transform);
                }
                return damage;
            default:
                damage = 0;
                return damage;
        }
    }
    public override void SpecialBehaviourStay(GameObject other,GameObject prefab){

    }
    public override void SpecialBehaviourExit(GameObject other,GameObject prefab){

    }
    //En vez de crear una explosion cuando se queda sin vida, llama a la funcion CreateSubmissile segun la cantidad de misiles a spawnear
    public override void ExplosionBehaviour(Transform origin){
        GameObject newExplosion = ExplosionPool.Instance.RequestExplosion(base.explosion);
        newExplosion.transform.position = origin.position;
    }

    //Crea un nuevo misil del tipo guardado y se crea una nueva direccion aleatoria, luego se lo manda en esa direccion
    private void CreateSubmissile(Transform origin){
        GameObject newMissile =  MissilePool.Instance.RequestMissile(subMissiles);
        newMissile.transform.position = origin.position;
        Rigidbody2D rb2D = newMissile.GetComponent<Rigidbody2D>();
        if(rb2D != null){
            Vector2 direction = new Vector2(Random.Range(0f,1f), Random.Range(0f,1f));
            rb2D.AddForce(direction * force);
        }
    }

}
