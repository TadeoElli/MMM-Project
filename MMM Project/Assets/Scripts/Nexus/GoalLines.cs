using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLines : MonoBehaviour
{
    // Start is called before the first frame update
    NexusStats nexusStats;
    List<GameObject> enemyList;
    [SerializeField] private bool isOnLeft;
    void Start()
    {
        nexusStats = FindObjectOfType<NexusStats>();
        enemyList = new List<GameObject>();
        
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(isOnLeft){
            if(other.gameObject.CompareTag("Enemy") && !enemyList.Contains(other.gameObject)){
                if(other.GetComponent<EnemyBehaviour>().normalDir){
                    enemyList.Add(other.gameObject);
                    nexusStats.ReduceLives();
                }
                
            }
        }
        else{
           if(other.gameObject.CompareTag("Enemy") && !enemyList.Contains(other.gameObject)){
                if(!other.GetComponent<EnemyBehaviour>().normalDir){
                    enemyList.Add(other.gameObject);
                    nexusStats.ReduceLives();
                }
                
            }
        }
    }
}
