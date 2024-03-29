using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusPosition : MonoBehaviour
{
    [SerializeField] private List<Transform> positions;

    private int index;

    public Vector3 SetPosition(){
        return positions[index].position;
    }

    public void SetNexusLeft(){
        index = 0;
    }
    public void SetNexusMiddle(){
        index = 1;
    }
    public void SetNexusRight(){
        index = 2;
    }
}
