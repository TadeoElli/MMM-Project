using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Missile", menuName = "ScriptableObject/Missiles/Basic", order = 0)]
public class BasicMissilesStrategy : MissileStrategy
{
    
    public override  void CreateMissile(){
        Debug.Log("Create Missile");
    }
}
