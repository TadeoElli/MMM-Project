using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Missile", menuName = "ScriptableObject/Missiles/Singularity", order = 4)]
public class SingularityMissileBehaviour : MissileStrategy
{
    [SerializeField] private float radius, force;
    [SerializeField] private bool isActive;
    public override GameObject CreateMissile(Transform origin){
        GameObject missile = MissilePool.Instance.RequestMissile(prefab);
        missile.transform.position = origin.position;
        return  missile;
    }
    public override void SpecialBehaviourEnter(GameObject other, GameObject prefab){

    }
    private void OnEnable() {
        isActive = false;
    }
    public override int CollisionBehaviour(GameObject other, GameObject prefab){
        int layer = other.layer;
        int damage;
        switch (layer)
        {
            case 7:
                damage = DamageTypesForMissiles.Instance.damageDictionary["Walls"];
                Rigidbody2D rb2D = prefab.GetComponent<Rigidbody2D>();
                Vector2 bounceDirection = rb2D.velocity;
                bounceDirection.y = bounceDirection.y * -1;

                rb2D.velocity = bounceDirection;
                return damage;
            case 8:
                damage = DamageTypesForMissiles.Instance.damageDictionary["SmallEnemies"];
                return damage;
            case 9:
                damage = DamageTypesForMissiles.Instance.damageDictionary["MediumEnemies"];
                return damage;
            case 10:
                damage = DamageTypesForMissiles.Instance.damageDictionary["BigEnemies"];
                return damage;
            case 11:
                damage = DamageTypesForMissiles.Instance.damageDictionary["Bosses"];
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
