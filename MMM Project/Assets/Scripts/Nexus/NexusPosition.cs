using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusPosition : MonoBehaviour
{
    [SerializeField] private List<Transform> positions;

    [SerializeField] private int index;
    void Start(){
        Nexus.Instance.transform.position = positions[index].position;;
    }
    public void SetPosition(int index){
        Nexus.Instance.transform.position = positions[index].position;
    }

}
