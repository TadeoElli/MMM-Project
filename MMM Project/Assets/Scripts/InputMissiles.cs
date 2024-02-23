using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InputMissiles : MonoBehaviour 
{
    public Observer<int> missileIndex = new Observer<int>(0);
    //[SerializeField] private int missileIndex;

    private void Start() {
        missileIndex.Invoke();
    }
    public void SetMissileIndex(int index){
        missileIndex.Value = index;
    }
}
