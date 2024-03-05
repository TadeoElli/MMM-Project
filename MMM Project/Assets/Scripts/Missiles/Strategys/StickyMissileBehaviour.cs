using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Missile", menuName = "ScriptableObject/Missiles/Sticky", order = 2)]
public class StickyMissileBehaviour : MissileStrategy
{
    [SerializeField] private GameObject enemyPierced;
    [SerializeField] private float force;
    private Vector2 direction;
    public override GameObject CreateMissile(Transform origin){
        GameObject missile = MissilePool.Instance.RequestMissile(prefab);
        missile.transform.position = origin.position;
        return  missile;
    }
    public override void SpecialBehaviourEnter(GameObject other, GameObject prefab){
        enemyPierced = other.gameObject;
        Rigidbody2D rb2D = prefab.GetComponent<Rigidbody2D>();
        if(rb2D != null){
            Vector2 direction = enemyPierced.transform.position - prefab.transform.position;
            float distance = 1 + direction.magnitude;
            float finalForce = force / distance;
            rb2D.AddForce(direction * finalForce);
        }
    }
    private void OnEnter(GameObject other, GameObject prefab){
        Rigidbody2D rb2D = other.GetComponent<Rigidbody2D>();
        if(rb2D != null){
            Vector2 direction = other.transform.position - prefab.transform.position;
            float distance = 1 + direction.magnitude;
            float finalForce = force / distance;
            rb2D.AddForce(direction * finalForce);
        }
    }
    public override int CollisionBehaviour(GameObject other, GameObject prefab){
        int layer = other.layer;
        int damage;
        switch (layer)
        {
            case 7:
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                Rigidbody2D rb2D = prefab.GetComponent<Rigidbody2D>();
                Vector2 bounceDirection = rb2D.velocity;
                bounceDirection.y = bounceDirection.y * -1;

                rb2D.velocity = bounceDirection;
                return damage;
            case 8:
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                enemyPierced = other.gameObject;
                OnEnter(other.gameObject, prefab.gameObject);
                DealDamage(other);
                return damage;
            case 9:
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                enemyPierced = other.gameObject;
                OnEnter(other.gameObject, prefab.gameObject);
                DealDamage(other);
                return damage;
            case 10:
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                enemyPierced = other.gameObject;
                OnEnter(other.gameObject, prefab.gameObject);
                DealDamage(other);
                return damage;
            case 11:
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                enemyPierced = other.gameObject;
                OnEnter(other.gameObject, prefab.gameObject);
                DealDamage(other);
                return damage;
            default:
                damage = 0;
                return damage;
        }
    }
    public override void SpecialBehaviourStay(GameObject other,GameObject prefab){
        if (other.gameObject == enemyPierced)
        {
            direction = ((Vector2)other.transform.position - (Vector2)prefab.transform.position).normalized;
        }
    }
    public override void SpecialBehaviourExit(GameObject other,GameObject prefab){
        if (other.gameObject == enemyPierced)
        {
            enemyPierced = null;
            prefab.GetComponent<Rigidbody2D>().velocity = direction * 5;
            
        }
    }
}
