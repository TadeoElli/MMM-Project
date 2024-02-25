using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingularityMissile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float radius, force;
    private bool isActive;

    // Update is called once per frame
    private void OnEnable() {
        isActive = false;
    }
    void Update()
    {
        if(isActive){
            AtractEnemies();
        }
        else{
            if(gameObject.GetComponent<Rigidbody2D>().velocity != Vector2.zero){

                isActive = true;
            }
        }
    }

    private void AtractEnemies(){
        Collider2D[] nearEnemies = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D enemy in nearEnemies)
        {
            if(enemy.CompareTag("Enemy")){
                Vector2 direction = (transform.position - enemy.transform.position).normalized;

                enemy.transform.position = Vector2.Lerp(enemy.transform.position, transform.position, force * Time.deltaTime);
            }
        }
    }
}
