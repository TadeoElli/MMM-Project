using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputMissiles : Subject
{
    // Start is called before the first frame update
    [SerializeField] private int missileIndex;

    public void SetMissileIndex(int index){
        missileIndex = index;
        NotifyObservers(missileIndex);
    }
}
