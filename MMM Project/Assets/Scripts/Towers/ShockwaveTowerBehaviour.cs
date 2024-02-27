using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Tower", menuName = "ScriptableObject/Tower/Shockwave", order = 0)]
public class ShockwaveTowerBehaviour : TowerStrategy
{
    public override void CreateTower(Vector2 origin){
        prefab.SetActive(true);
        prefab.transform.position = origin;
    }
    public override void SpecialBehaviour(GameObject prefab){

    }
    
}
