using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingularityExplosion : MonoBehaviour
{
    ///Este tipo de explosion tiene un collider para que cuando lo toque un enemigo este se desactive
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy")){
            other.gameObject.SetActive(false);
        }
    }
}
