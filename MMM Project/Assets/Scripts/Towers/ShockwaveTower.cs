using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Tower", menuName = "ScriptableObject/Tower/Shockwave", order = 0)]
public class ShockwaveTowerBehaviour : TowerStrategy
{
    public override void CreateTower(Transform origin){
        prefab.SetActive(true);
        prefab.transform.position = origin.position;
    }
    public override void SpecialBehaviour(GameObject prefab){

    }
    
}
