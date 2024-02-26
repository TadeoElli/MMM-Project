using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InputController : MonoBehaviour 
{
    public Observer<int> missileIndex = new Observer<int>(0);
    public Observer<int> towerIndex = new Observer<int>(0);
    //[SerializeField] private int missileIndex;


    public void SetMissileIndex(int index){
        missileIndex.Value = index;
    }
    public void SetTowerIndex(int index){
        towerIndex.Value = index;
    }
}
