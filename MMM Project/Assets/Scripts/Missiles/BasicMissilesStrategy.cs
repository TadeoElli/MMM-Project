using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Missile", menuName = "ScriptableObject/Missiles/Basic", order = 0)]
public class BasicMissilesStrategy : MissileStrategy
{
    public override GameObject CreateMissile(Transform origin){
        GameObject missile = MissilePool.Instance.RequestMissile();
        missile.transform.position = origin.position;
        return  missile;
    }

    public override void Shoot(){
        Debug.Log("Shoot");
    }


}
