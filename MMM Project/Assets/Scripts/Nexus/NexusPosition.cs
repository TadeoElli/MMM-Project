using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusPosition : MonoBehaviour
{
    [SerializeField] public List<GameObject> positions;

    public int index;

    public Vector3 SetPosition(){
        return positions[index].transform.position;
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
