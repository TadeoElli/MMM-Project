using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Missile", menuName = "ScriptableObject/Missiles/Singularity", order = 3)]
public class SingularityMissileBehaviour : MissileStrategy
{
    [SerializeField] private float force;

    public override int CollisionBehaviour(GameObject other, GameObject prefab){
        int layer = other.layer;
        int damage;
        if(layer == 7){
            damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
            Rigidbody2D rb2D = prefab.GetComponent<Rigidbody2D>();
            Vector2 bounceDirection = rb2D.velocity;
            bounceDirection.y = bounceDirection.y * -1;

            rb2D.velocity = bounceDirection;
            return damage;
        }
        else{
            return 0;
        }
    }
    public override void SpecialBehaviourStay(GameObject other,GameObject prefab){
        Vector2 direction = (prefab.transform.position - other.transform.position).normalized;
        other.transform.position = Vector2.Lerp(other.transform.position, prefab.transform.position, force * Time.deltaTime);
    }
    public override void SpecialBehaviourExit(GameObject other,GameObject prefab){
    }
}
