using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Missile", menuName = "ScriptableObject/Missiles/Angle", order = 1)]
public class RandomAngleMissileBehaviour : MissileStrategy
{

    private void OnEnter(GameObject prefab){
        Rigidbody2D rigidbody2D = prefab.GetComponent<Rigidbody2D>();
        float actualAngle = Mathf.Atan2(rigidbody2D.velocity.y, rigidbody2D.velocity.x);
        float newAngle = actualAngle + Random.Range(-Mathf.PI / 2f, Mathf.PI / 2f);
        Vector2 newDirection = new Vector2(Mathf.Cos(newAngle), Mathf.Sin(newAngle));
        //Debug.Log(newDirection);
        rigidbody2D.velocity = newDirection * 5;
    }
    public override int CollisionBehaviour(GameObject other, GameObject prefab){
        int layer = other.layer;
        int damage;
        switch (layer)
        {
            case 7:
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                return damage;
            case 8:
            case 9:
            case 10:
            case 11:
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                OnEnter(prefab);
                DealDamage(other);
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
}
