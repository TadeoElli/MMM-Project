using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private EnemyStrategy enemy;
    [SerializeField] private float life, rotationSpeed, speed;
    [SerializeField] private float direction;
    [SerializeField] private bool canMove = true;
    private float timer;
    private Rigidbody2D rigidbody2D;

    void Start() {
        // Inicializa con la estrategia de movimiento constante
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Awake() {
    }
    private void OnEnable() {
        //life = enemy.maxLife;
    }

    void Update() {
        // Aplica la estrategia de movimiento actual
        //Debug.Log(transform.eulerAngles.z);
        if(canMove){
            if(rigidbody2D.velocity.magnitude < 0.2){
                timer = timer + 1 * Time.deltaTime;
                if(timer > 1.5f){
                    Rotate();
                    if(transform.eulerAngles.z > (direction - 90) && transform.eulerAngles.z < (direction + 90)){
                        MoveForward();
                    }
                }
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Missiles")){
            timer = 0;
        }
    }

    public void MoveForward() {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    private void Rotate() {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, direction), rotationSpeed * Time.deltaTime);
    }


}
