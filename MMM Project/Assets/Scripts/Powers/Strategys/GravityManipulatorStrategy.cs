using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Power", menuName = "ScriptableObject/Power/GravityManipulator", order = 0)]
public class GravityManipulatorStrategy : PowerStrategy
{
    /// <summary>
    /// Este tipo de poder se basa en manipular a un enemigo, agarrandolo y lanzandolo o haciendo que se choque con otros objetos
    /// Dependiendo del nivel de este poder es el tipo de enemigo que podra agarrar (Pequeños, medianos o grandes)
    /// </summary>
    [Header("Special Properties")]
    private Rigidbody2D rb;
    private EnemyBehaviour enemy;   //el enemigo que va a manipular
    [SerializeField] private int level = 0; //el nivel de la habilidad
    [SerializeField] private float wallTop, wallBottom;     //El valor de la posicion de las paredes
    [SerializeField] private Sprite performedSprite;    //La imagen del cursor mientras me mantiene presionado
    [SerializeField] private Material performedMaterial;    //el material del cursor mientras se mantiene presionado
    public int Level{get{return level;} set{level = Mathf.Clamp(value,0,3);}}

    public override bool BehaviourStarted(){    //El comportamiento cuando se presiona
        // Convertir la posición del clic del ratón a un rayo en el mundo 2D
        Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);
        
        // Verificar si el objeto colisionado es un enemigo
        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            // Si es un enemigo, reviso que el enemigo sea del tipoadecuado para el nivel del poder y si es asi lo activo, con el cursor de la escala correspondiente
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

    private void Activate(GameObject other){//Toma el componente EnemiBehaviour del enemigo y le establece que no se puede mover, ademas de que desactiva su rigidBody
        enemy = other.GetComponent<EnemyBehaviour>();
        rb = other.GetComponent<Rigidbody2D>();
        enemy.canMove = false;
        Debug.Log("Gravity Manipulator");
    }
    public override bool BehaviourPerformed(){  //Mientras se mantiene presionado y el enemigo que guardo existe
        if(enemy != null){
            if(rb.gameObject.activeSelf){   //Si tiene RigidBody
                // Obtener la posición del ratón en el mundo
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
    public override void BehaviourEnded(){  //cuando se deja de presionar, se activa la posibilida de moderse del enemigo
        if(enemy != null){
            enemy.canMove = true;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//toma la ultima posicion del mouse
            Vector2 direction = mousePosition - rb.position;//Y mueve al enemigo lanzandolo hacia esa posicion
            rb.AddForce(direction.normalized * 100f, ForceMode2D.Impulse);
        }
    }
}
