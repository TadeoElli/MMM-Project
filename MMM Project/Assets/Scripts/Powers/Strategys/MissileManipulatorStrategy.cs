using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Power", menuName = "ScriptableObject/Power/MissileManipulator", order = 1)]
public class MissileManipulatorStrategy : PowerStrategy
{
    [Header("Special Properties")]
    private Rigidbody2D rb;
    private MissileBehaviour misile;
    [SerializeField] private float wallTop, wallBottom, maxTime;
    private float timer = 0;
    [SerializeField] private Sprite performedSprite;
    [SerializeField] private Material performedMaterial;
    public override bool BehaviourStarted(){
        // Convertir la posición del clic del ratón a un rayo en el mundo 2D
        Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);
        
        // Verificar si el objeto colisionado es un misil
        if (hit.collider != null && hit.collider.CompareTag("Missiles"))
        {
            // Si es un misil, activar el poder que se activa al hacer clic en un enemigo
            Activate(hit.collider.gameObject);
            CursorController.Instance.SetCursor(performedSprite, performedMaterial, scale);
            timer = 0;
            return true;
        }
        else{
            Debug.Log("Invalid Action");
            return false;
        }
    }

    private void Activate(GameObject other){
        misile = other.GetComponent<MissileBehaviour>();
        rb = other.GetComponent<Rigidbody2D>();
        Debug.Log("Misile Manipulator");
    }
    private void Desactivate(){
        if(misile != null){
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - rb.position;

            rb.AddForce(direction.normalized * 20f, ForceMode2D.Impulse);
        }
    }
    public override bool BehaviourPerformed(){
        if(misile != null){
            if(timer < maxTime){
                if(rb.gameObject.activeSelf){
                    // Obtener la posición del ratón en el mundo
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if(mousePosition.y > wallTop || mousePosition.y < wallBottom){
                        misile.TakeDamage(5000);
                    }
                    // Actualizar la posición del enemigo al ratón
                    rb.MovePosition(mousePosition);
                    timer = timer + 1 * Time.deltaTime;
                    return true;
                }
                else{
                    return false;
                }
            }
            else{
                Desactivate();
                return false;
            }
        }
        else{
            return false;
        }
    }
    public override void BehaviourEnded(){
        Desactivate();
        
    }
}
