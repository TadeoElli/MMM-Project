using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InputController : MonoBehaviour 
{
    public Observer<int> missileIndex = new Observer<int>(0);
    public Observer<int> towerIndex = new Observer<int>(0);
    //[SerializeField] private int missileIndex;
    public Observer<int> powerIndex = new Observer<int>(0);

    public bool isAvailable = true;

    public void SetMissileIndex(int index){
        if(isAvailable){
            missileIndex.Value = index;
        }
        
    }
    public void SetTowerIndex(int index){
        if(isAvailable){
            towerIndex.Value = index;
        }
    }
    public void SetPowerIndex(int index){
        if(isAvailable){
            powerIndex.Value = index;
        }
    }
}
