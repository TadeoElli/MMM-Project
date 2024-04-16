using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SingularityExplosion : MonoBehaviour
{
    ///Este tipo de explosion tiene un collider para que cuando lo toque un enemigo este se desactive
    [SerializeField] private EnemySpawner spawner;

    private void Start() {
        spawner = FindObjectOfType<EnemySpawner>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy")){
            if(spawner != null){spawner.ReduceEnemiesAlive(1);}
            other.gameObject.SetActive(false);
        }
    }
}
