using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : Subject
{
    // Start is called before the first frame update
    [SerializeField] private int missileIndex;
    [SerializeField] private int powers;
    [SerializeField] private int turrets;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            missileIndex = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2)){
            missileIndex = 1;
        }
    }
}
