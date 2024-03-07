using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Power", menuName = "ScriptableObject/Power/GravityManipulator", order = 0)]
public class GravityManipulatorStrategy : PowerStrategy
{
    private Rigidbody2D rb;
    private EnemyBehaviour enemy;
    public override bool BehaviourStarted(){
        // Convertir la posición del clic del ratón a un rayo en el mundo 2D
        Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);
        
        // Verificar si el objeto colisionado es un enemigo
        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            // Si es un enemigo, activar el poder que se activa al hacer clic en un enemigo
            enemy = hit.collider.GetComponent<EnemyBehaviour>();
            rb = hit.collider.GetComponent<Rigidbody2D>();
            enemy.canMove = false;
            Debug.Log("Gravity Manipulator");
            return true;
            
        }
        else{
            Debug.Log("Invalid Action");
            return false;
        }
    }
    public override bool BehaviourPerformed(){

        // Obtener la posición del ratón en el mundo
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Actualizar la posición del enemigo al ratón
        rb.MovePosition(mousePosition);
        return true;
    }
    public override void BehaviourEnded(){
        enemy.canMove = true;
    }
}
