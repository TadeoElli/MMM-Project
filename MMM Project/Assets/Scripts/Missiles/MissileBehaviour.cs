using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{
    [SerializeField] private MissileStrategy missile;   //El Strategy de los misiles
    [SerializeField] private float life;    //La vida del misil
    private bool oneChance = true;  //Este bool sirve para que cuando baje de 0 la vida se quede con 1 asi puede llegar a un rebote mas
    [SerializeField] private bool isSpecial = false;    //Sirve para diferenciar a los misiles que tengan un comportamiento especial o no
    //private int damage;
    private float  minStability, maxStability;  //La estabilidad minima y maxima
    [HideInInspector] public bool hasBeenAtracted = false;      //Sirve para identificar si fue atraido o no por una torre
    private float rotationSpeed = 100f;     //La velocidad a la que esta rotando en esa torre
    public float RotationSpeed{get{return rotationSpeed;} set{ rotationSpeed =  Mathf.Clamp(value, 100f, 700f); }}
    [HideInInspector] private float rotationDirection;  //La direccion (Si entro por arriba es positiva, si no negativa)
    public float RotationDirection{get{return rotationDirection;}set{ rotationDirection =  Mathf.Clamp(value, -1f, 1f);}}
    private CircleCollider2D circleCollider2D;
    private Rigidbody2D rb2D;
    [Header("Sounds Effects")]
    [SerializeField] private AudioClip launchClip, bounceClip;

    private void OnEnable() {   //Declaro las estadisticas
        life = missile.maxLife;
        minStability = missile.minStability;
        maxStability = missile.maxStability;
        oneChance = true;
        circleCollider2D = GetComponent<CircleCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage){   //Funcion que toma el daño y si es menor a 0 crea la explosion
        life -= damage;
        if(life<= 0 && oneChance){
            oneChance = false;
            life = 1;
        }
        if(life <= 0){
            missile.ExplosionBehaviour(transform);
            this.gameObject.SetActive(false);
        }
    }

     //Calcula la probabilidad de exito de que se dispare el misil, en funcion de cuanto se estira, si falla, explota el misil
    public void TryToShoot(Vector2 startPoint, Vector2 endPoint, int baseStability){   
        float distance = Vector2.Distance(startPoint, endPoint);
        float probability = Random.Range(0,100);
        float maxProbability;
        if(distance <= 1){
            maxProbability = maxStability;
            maxProbability = maxProbability + (baseStability * 3.5f);
        }
        else{
            distance  = distance - 1;
            maxProbability = maxStability + (minStability - maxStability) * distance;
            maxProbability = Mathf.Clamp(maxProbability, minStability, maxStability);
            maxProbability = maxProbability + (baseStability * 3.5f);
        }
        if(probability > maxProbability){
            missile.ExplosionBehaviour(transform);
            this.gameObject.SetActive(false);
        }
        else{
            AudioManager.Instance.PlaySoundEffect(launchClip);
        }
    }

    //Comportamiento cuando collisiona con un objeto
    private void OnCollisionEnter2D(Collision2D other) {
        float damage = missile.CollisionBehaviour(other.gameObject, this.gameObject);
        if(damage > 0){
            TakeDamage(damage);
        }
        AudioManager.Instance.PlaySoundEffect(bounceClip);
        //Debug.Log(rb2D.velocity.magnitude);
    }
    //Comportamiento cuando colisiona con un objeto y el collider del misil es trigger(Debido a algunas funciones)
    private void OnTriggerEnter2D(Collider2D other) {
        if(circleCollider2D.isTrigger){
            float damage = missile.CollisionBehaviour(other.gameObject, this.gameObject);
            TakeDamage(damage);
            AudioManager.Instance.PlaySoundEffect(bounceClip);
        //Debug.Log(rb2D.velocity.magnitude);
        }
    }
    //Comportamiento mientras esta colisionando
    private void OnTriggerStay2D(Collider2D other) {
        if(circleCollider2D.isTrigger){
            if(isSpecial){
                if (other.CompareTag("Enemy"))
                {
                    missile.SpecialBehaviourStay(other.gameObject, this.gameObject);
                }
            }
        }
    }
    //comportamiento cuando sale del trigger
    private void OnTriggerExit2D(Collider2D other) {
        if(circleCollider2D.isTrigger){
            if(isSpecial){
                if (other.CompareTag("Enemy"))
                {
                    missile.SpecialBehaviourExit(other.gameObject, this.gameObject);
                }
            }
        }
    }




}
