using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Power", menuName = "ScriptableObject/Power/GravityManipulator", order = 0)]
public class GravityManipulatorStrategy : PowerStrategy
{
    [Header("Special Properties")]
    private Rigidbody2D rb;
    private EnemyBehaviour enemy;
    [SerializeField] private int level = 0;
    [SerializeField] private float wallTop, wallBottom;
    [SerializeField] private Sprite performedSprite;
    [SerializeField] private Material performedMaterial;

    public override bool BehaviourStarted(){
        // Convertir la posición del clic del ratón a un rayo en el mundo 2D
        Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);
        
        // Verificar si el objeto colisionado es un enemigo
        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            // Si es un enemigo, activar el poder que se activa al hacer clic en un enemigo
            if(hit.collider.gameObject.layer == 8 && level >= 1){
                Activate(hit.collider.gameObject);
                CursorController.Instance.SetCursor(performedSprite, performedMaterial, new Vector3(0.25f,0.25f,1f));
                return true;
            }
            else if(hit.collider.gameObject.layer == 9 && level >= 2){
                Activate(hit.collider.gameObject);
                CursorController.Instance.SetCursor(performedSprite, performedMaterial, new Vector3(0.35f,0.35f,1f));
                return true;
            }
            else if(hit.collider.gameObject.layer == 10 && level >= 3){
                Activate(hit.collider.gameObject);
                return true;
            }
            else{
                return false;
            }
        }
        else{
            Debug.Log("Invalid Action");
            return false;
        }
    }

    private void Activate(GameObject other){
        enemy = other.GetComponent<EnemyBehaviour>();
        rb = other.GetComponent<Rigidbody2D>();
        enemy.canMove = false;
        Debug.Log("Gravity Manipulator");
    }
    public override bool BehaviourPerformed(){
        if(enemy != null){
            if(rb.gameObject.activeSelf){
                // Obtener la posición del ratón en el mundo
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if(mousePosition.y > wallTop || mousePosition.y < wallBottom){
                    enemy.TakeDamage(5000);
                }
                // Actualizar la posición del enemigo al ratón
                rb.MovePosition(mousePosition);
                return true;
            }
            else{
                return false;
            }
        }
        else{
            return false;
        }
    }
    public override void BehaviourEnded(){
        if(enemy != null){
            enemy.canMove = true;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - rb.position;
            rb.AddForce(direction.normalized * 100f, ForceMode2D.Impulse);
        }
    }
}
