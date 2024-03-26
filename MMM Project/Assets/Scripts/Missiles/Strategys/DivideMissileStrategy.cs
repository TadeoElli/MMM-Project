using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Missile", menuName = "ScriptableObject/Missiles/Divide", order = 5)]
public class DivideMissileStrategy : MissileStrategy
{
    [SerializeField] MissileBehaviour subMissiles;
    [SerializeField] private float force;
    [SerializeField] private int cantOfSubmissiles;
    
    public override int CollisionBehaviour(GameObject other, GameObject prefab){
        MissileBehaviour missileBehaviour = prefab.GetComponent<MissileBehaviour>();
            
        int layer = other.layer;
        int damage;
        switch (layer)
        {
            case 7:
                damage = 0;
                Rigidbody2D rb2D = prefab.GetComponent<Rigidbody2D>();
                Vector2 bounceDirection = rb2D.velocity;
                bounceDirection.y = bounceDirection.y * -1;

                rb2D.velocity = bounceDirection;
                return damage;
            case 8:
            case 9:
            case 10:
            case 11:
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                missileBehaviour.TakeDamage(10);
                missileBehaviour.TakeDamage(10);
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
    public override void ExplosionBehaviour(Transform origin){
        for (int i = 0; i < cantOfSubmissiles; i++)
        {
            CreateSubmissile(origin);
        }
    }

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
