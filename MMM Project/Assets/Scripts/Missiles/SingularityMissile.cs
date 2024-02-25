using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingularityMissile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private MissileStrategy missile;
    [SerializeField] private float radius, force;
    [SerializeField] private bool isActive = false;
    private Vector2 direction;

    // Update is called once per frame
    private void OnEnable() {
        isActive = false;
    }
    void Update()
    {
        if(isActive){

            Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, radius);

            foreach (Collider2D collisions in objetos){

                if(collisions.gameObject.tag == "Enemy"){
                    Rigidbody2D rb2D = collisions.GetComponent<Rigidbody2D>();
                    if(rb2D != null){
                        Vector2 direction = collisions.transform.position - transform.position;
                        float distance = 1 + direction.magnitude;
                        float finalForce = force / distance;
                        rb2D.AddForce((direction * -1) * finalForce);   
                    }
                }
            }
        }
        else{
            if(gameObject.GetComponent<Rigidbody2D>().velocity != Vector2.zero){
                isActive = true;
            }
        }
    }
}
