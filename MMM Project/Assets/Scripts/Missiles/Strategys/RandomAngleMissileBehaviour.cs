using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Missile", menuName = "ScriptableObject/Missiles/Angle", order = 1)]
public class RandomAngleMissileBehaviour : MissileStrategy
{
    public override GameObject CreateMissile(Transform origin){
        GameObject missile = BasicMissilePool.Instance.RequestMissile(prefab);
        missile.transform.position = origin.position;
        return  missile;
    }
    public override void SpecialBehaviour(GameObject prefab){
        Rigidbody2D rigidbody2D = prefab.GetComponent<Rigidbody2D>();
        float actualAngle = Mathf.Atan2(rigidbody2D.velocity.y, rigidbody2D.velocity.x);
        float newAngle = actualAngle + Random.Range(-Mathf.PI / 2f, Mathf.PI / 2f);
        Vector2 newDirection = new Vector2(Mathf.Cos(newAngle), Mathf.Sin(newAngle));
        //Debug.Log(newDirection);
        rigidbody2D.velocity = newDirection * 5;
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
