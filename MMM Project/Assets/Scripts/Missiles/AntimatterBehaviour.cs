using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntimatterBehaviour : MonoBehaviour
{
    // Start is called before the first frame update


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy"){
            MissileBehavior missileBehaviour = GetComponent<MissileBehavior>();
            missileBehaviour.TakeDamage(10);
            missileBehaviour.TakeDamage(10);
        }
    }


}
