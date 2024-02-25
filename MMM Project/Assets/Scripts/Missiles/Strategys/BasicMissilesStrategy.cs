using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Missile", menuName = "ScriptableObject/Missiles/Basic", order = 0)]
public class BasicMissilesStrategy : MissileStrategy
{
    public override GameObject CreateMissile(Transform origin){
        GameObject missile = BasicMissilePool.Instance.RequestMissile(prefab);
        missile.transform.position = origin.position;
        return  missile;
    }
    public override void SpecialBehaviour(GameObject prefab){
    }

    public override int CollisionBehaviour(int layer){
        int damage;
        switch (layer)
        {
            case 7:
                damage = DamageTypesForMissiles.Instance.damageDictionary["Walls"];
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



}
