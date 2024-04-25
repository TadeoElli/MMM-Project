using UnityEngine;



[CreateAssetMenu(fileName = "New Power", menuName = "ScriptableObject/Power/MissileManipulator", order = 1)]
public class MissileManipulatorStrategy : PowerStrategy
{
    /// <summary>
    /// Este tipo de poder manipula la posicion de un misil ya lansado, haciendo que choque con los enemigos o relanzandolo a otra posicion
    /// </summary>
    [Header("Special Properties")]
    [SerializeField] private float wallTop, wallBottom, maxTime;    //La posicion de las paredes y el tiempo maximo que se puede controlar a un misil
    [SerializeField] private Sprite performedSprite;    //Imagen del cursor mientras se mantiene apretado
    [SerializeField] private Material performedMaterial;    //Material del cursor mientras se mantiene apretado
    private Rigidbody2D rb;
    private MissileBehaviour misile;
    private float timer = 0;    
    public override bool BehaviourStarted(){    //Comportamiento cuando se presiona
        // Convertir la posición del clic del ratón a un rayo en el mundo 2D
        Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);
        
        // Verificar si el objeto colisionado es un misil
        if (hit.collider != null && hit.collider.CompareTag("Missiles"))
        {
            // Si es un misil, activar el poder que se activa al hacer clic en un misil
            Activate(hit.collider.gameObject);
            CursorController.Instance.SetCursor(performedSprite, performedMaterial, scale);
            timer = 0;
            return true;
        }
        else{
            //Debug.Log("Invalid Action");
            AudioManager.Instance.PlaySoundEffect(invalidEffect);
            return false;
        }
    }

    private void Activate(GameObject other){    //Toma el componente del misil
        misile = other.GetComponent<MissileBehaviour>();
        rb = other.GetComponent<Rigidbody2D>();
        Debug.Log("Misile Manipulator");
    }
    private void Desactivate(){ //Si el misil que se guardo existe, al desactivarse toma la ultima posicion del mouse y lo impulsa hacia esa direccion
        if(misile != null){
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - rb.position;

            rb.AddForce(direction.normalized * 20f, ForceMode2D.Impulse);
        }
    }
    public override bool BehaviourPerformed(){  //El comportamiento mientras se mantiene presionado
        if(misile != null){ //Si hay un misil
            if(timer < maxTime){    //Y todavia no se termino el tiempo
                if(rb.gameObject.activeSelf){   
                    // Obtener la posición del ratón en el mundo
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    // Actualizar la posición del misil al ratón
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
    public override void BehaviourEnded(){  //El comportamiento cuando se deja de presionar
        Desactivate();
        
    }
}
