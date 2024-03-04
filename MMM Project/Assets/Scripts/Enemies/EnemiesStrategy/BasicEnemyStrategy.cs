using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemies/Basic", order = 0)]
public class BasicEnemyStrategy : EnemyStrategy
{
    public override GameObject CreateEnemy(Transform origin){
        GameObject enemy = MissilePool.Instance.RequestMissile(prefab);
        enemy.transform.position = origin.position;
        return  enemy;
    }
    
    public override int CollisionBehaviour(GameObject other, GameObject prefab){            
        int layer = other.layer;
        int damage;
        switch (layer)
        {
            case 7:
                damage = 0;
                return damage;
            case 8:
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                return damage;
            case 9:
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                return damage;
            case 10:
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                return damage;
            case 11:
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                return damage;
            default:
                damage = 0;
                return damage;
        }
    }

}

