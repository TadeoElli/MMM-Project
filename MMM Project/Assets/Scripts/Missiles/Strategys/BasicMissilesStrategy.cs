using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Missile", menuName = "ScriptableObject/Missiles/Basic", order = 0)]
public class BasicMissilesStrategy : MissileStrategy
{
    public override GameObject CreateMissile(Transform origin){
        GameObject missile = MissilePool.Instance.RequestMissile(prefab);
        missile.transform.position = origin.position;
        return  missile;
    }
    public override void SpecialBehaviourEnter(GameObject other, GameObject prefab){
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
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                DealDamage(other);
                return damage;
            case 9:
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                DealDamage(other);
                return damage;
            case 10:
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                DealDamage(other);
                return damage;
            case 11:
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                DealDamage(other);
                return damage;
            default:
                damage = 0;
                return damage;
        }
    }

    /*private void DealDamage(GameObject other){
        float damage = Random.Range(minDamage,maxDamage);
        if (other.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemy))
        {
            enemy.TakeDamage(damage);
        }
    }*/
    public override void SpecialBehaviourStay(GameObject other,GameObject prefab){

    }
    public override void SpecialBehaviourExit(GameObject other,GameObject prefab){

    }
        
    


}
