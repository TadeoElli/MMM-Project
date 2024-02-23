using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Missile", menuName = "ScriptableObject/Missiles/Special", order = 1)]
public class SpecialMissileBehaviour : MissileStrategy
{
    public override GameObject CreateMissile(Transform origin){
        GameObject missile = BasicMissilePool.Instance.RequestMissile(prefab);
        missile.transform.position = origin.position;
        return  missile;
    }
    public override void SpecialBehaviour(Rigidbody2D rigidbody2D){
        float actualAngle = Mathf.Atan2(rigidbody2D.velocity.y, rigidbody2D.velocity.x);
        float newAngle = actualAngle + Random.Range(-Mathf.PI / 2f, Mathf.PI / 2f);
        Vector2 newDirection = new Vector2(Mathf.Cos(newAngle), Mathf.Sin(newAngle));
        Debug.Log(newDirection);
        rigidbody2D.velocity = newDirection * 5;
    }
    


}
