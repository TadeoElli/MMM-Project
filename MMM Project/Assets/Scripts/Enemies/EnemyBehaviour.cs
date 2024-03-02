using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    //[SerializeField] private EnemyStrategy enemy;
    [SerializeField] private float life, rotationSpeed, speed;
    [SerializeField] private float direction;
    [SerializeField] private bool isRotating = false;
    private float timer;
    private Rigidbody2D rigidbody2D;
    IMovementStrategy movementStrategy;

    void Start() {
        // Inicializa con la estrategia de movimiento constante
        rigidbody2D = GetComponent<Rigidbody2D>();
        movementStrategy = new ConstantMovementStrategy();
    }

    private void Awake() {
    }
    private void OnEnable() {
        //life = enemy.maxLife;
    }

    void Update() {
        // Aplica la estrategia de movimiento actual
        Debug.Log(transform.eulerAngles.z);
        movementStrategy.Rotate(rotationSpeed, direction, rigidbody2D);
        if(transform.eulerAngles.z > (direction - 10) && transform.eulerAngles.z < (direction + 10)){
            if(timer > 1){
                movementStrategy.MoveForward(speed, rigidbody2D);
            }
            else
            {
                timer = timer + 1 * Time.deltaTime;
            }
        }

    }


}
