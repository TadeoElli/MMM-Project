using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingularityExplosion : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy")){
            other.gameObject.SetActive(false);
        }
    }
}
