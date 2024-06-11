using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissileBehaviour : MonoBehaviour, IGridEntity
{//IA2-P2”.
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
    public event Action<IGridEntity> OnMove;

    private void OnEnable() {   //Declaro las estadisticas
        life = missile.maxLife;
        minStability = missile.minStability;
        maxStability = missile.maxStability;
        oneChance = true;
        circleCollider2D = GetComponent<CircleCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        IGridEntity gridEntity = GetComponent<IGridEntity>();
        if (gridEntity != null) {
            SpatialGrid.Instance.Add(gridEntity);
        }
    }

    public void TakeDamage(float damage){   //Funcion que toma el daño y si es menor a 0 crea la explosion
        life -= damage;
        if(life<= 0 && oneChance){
            oneChance = false;
            life = 1;
        }
        if(life <= 0){
            missile.ExplosionBehaviour(transform);
            gameObject.SetActive(false);
            IGridEntity gridEntity = GetComponent<IGridEntity>();
            if (gridEntity != null) {
                SpatialGrid.Instance.Remove(gridEntity);
            }
        }
    }

     //Calcula la probabilidad de exito de que se dispare el misil, en funcion de cuanto se estira, si falla, explota el misil
    public void TryToShoot(Vector2 startPoint, Vector2 endPoint, int baseStability){   
        float distance = Vector2.Distance(startPoint, endPoint);
        float probability = UnityEngine.Random.Range(0,100);
        float maxProbability = distance <= 1 ? maxStability + (baseStability * 3.5f): Mathf.Clamp(maxStability + (minStability - maxStability) * (distance - 1), minStability, maxStability) + (baseStability * 3.5f);
        if(probability > maxProbability){
            missile.ExplosionBehaviour(transform);
            gameObject.SetActive(false);
        }
        else{
            AudioManager.Instance.PlaySoundEffect(missile.launchEffect);
        }
    }
    private void Update() {
        OnMove?.Invoke(this);
    }

    //Comportamiento cuando collisiona con un objeto
    private void OnCollisionEnter2D(Collision2D other) {
        float damage = missile.CollisionBehaviour(other.gameObject, this.gameObject);
        if(damage > 0){
            TakeDamage(damage);
        }
        //Debug.Log(rb2D.velocity.magnitude);
    }
    //Comportamiento cuando colisiona con un objeto y el collider del misil es trigger(Debido a algunas funciones)
    private void OnTriggerEnter2D(Collider2D other) {
        if(circleCollider2D.isTrigger){
            float damage = missile.CollisionBehaviour(other.gameObject, this.gameObject);
            TakeDamage(damage);
        //Debug.Log(rb2D.velocity.magnitude);
        }
    }
    //Comportamiento mientras esta colisionando
    private void OnTriggerStay2D(Collider2D other) {
        if (circleCollider2D.isTrigger && isSpecial && other.CompareTag("Enemy"))
        {
            missile.SpecialBehaviourStay(other.gameObject, gameObject);
        }
    }
    //comportamiento cuando sale del trigger
    private void OnTriggerExit2D(Collider2D other) {
        if (circleCollider2D.isTrigger && isSpecial && other.CompareTag("Enemy"))
        {
            missile.SpecialBehaviourExit(other.gameObject, gameObject);
        }
    }

    public Vector3 Position {
        get => transform.position;
        set => transform.position = value;
    }


}
