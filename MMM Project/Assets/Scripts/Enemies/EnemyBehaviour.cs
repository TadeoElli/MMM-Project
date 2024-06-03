using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FSM;

public class EnemyBehaviour : MonoBehaviour, IGridEntity
{
    #region Stats
    [SerializeField] private EnemyStrategy enemy;        //El Strategy
    public EnemyStrategy Enemy{get{return enemy;}}
    [SerializeField] private float life;        
    private float speed;
    private float direction;        //La direccion en la que se mueve
    private float rotationSpeed = 1;
    #endregion¨

    #region Components
    private Collider2D col;
    private Rigidbody2D rb2D;
    [SerializeField] private GameObject specialParticle;        //La particula que va a tener el enemigo si tiene un comportamiento especial
    private EnemyView view;
    #endregion

    public bool canMove = true;       //Variable que maneja si se puede mover o no, para desactivar con habilidades o comportamientos
    [HideInInspector] public bool normalDir = false;        //si se va a mover en la direccion normal (hacia la izquierda) o no, se setea desde el spawner
    public readonly bool absorb = false;       //Si el enemigo tiene la capacidad de absorber misiles


    #region Events
    public delegate void OnEnemyDeath(int amount);
    public OnEnemyDeath notifyKillCount, notifyScore;
    public event Action<IGridEntity> OnMove;
    #endregion

    #region FSM
    FiniteStateMachine fsm;
    [Header("States")]
    [SerializeField] EnemyIdleState idleState;
    [SerializeField] EnemyMoveState moveState;
    [SerializeField] EnemyRotateState rotateState;
    [SerializeField] EnemyDesactiveState desactiveState;
    #endregion

    private void Awake() {
        col = GetComponent<Collider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        view = GetComponent<EnemyView>();
    }
    private void Start()
    {//IA2-P3”.
        fsm = new FiniteStateMachine(idleState, StartCoroutine);
        direction = normalDir ? 90:270;     //Establezco segun normalDir que direccion va a tener el enemigo

        fsm.AddTransition(EnemyStateTransitions.ToRotate, idleState, rotateState);
        fsm.AddTransition(EnemyStateTransitions.ToMoveForward, idleState, moveState);
        fsm.AddTransition(EnemyStateTransitions.ToMoveForward, rotateState, moveState);
        fsm.AddTransition(EnemyStateTransitions.ToDesactivate, idleState, desactiveState);
        fsm.AddTransition(EnemyStateTransitions.ToDesactivate, rotateState, desactiveState);
        fsm.AddTransition(EnemyStateTransitions.ToDesactivate, moveState, desactiveState);
        fsm.AddTransition(EnemyStateTransitions.ToIdle, desactiveState, idleState);

        fsm.Active = true;
    }
    private void OnEnable() {
        life = enemy.maxLife;
        speed = enemy.velocity;
        col.enabled = false;
        //view.TakeDamageView(life, enemy.maxLife);
        StartCoroutine(DelayForActivateCollider());
    }

    IEnumerator DelayForActivateCollider(){     //Desactiva la colisision al spawnear y la activa desp de unos segundo para evitar choques al spawnear
        yield return new WaitForSeconds(2);
        col.enabled = true;
        view.SetHpImage(normalDir);
    }
    void Update() {
        // Aplica la estrategia de movimiento actual
        //Debug.Log(transform.eulerAngles.z);
        /*if(canMove){        //Si puede moverse
            if(rb2D.velocity.magnitude < 0.2){  //Si la velocidad del objeto es lo suficiente mente chica, procede a moverse, esto sirve para que 
            //cuando la velocidad aumente debido a una colision o un comportamiento, espere a que se detenga para retomar el movimiento, desp de un tiempo
                timer = timer + 1 * Time.deltaTime;
                if(timer > 1.5f){
                    Rotate();       //Rota el objeto hacia la direccion establecida
                    if (IsFacingDirection()){ //Si esta apuntando medianamente a esa dirreccion
                        MoveForward();      //Lo mueve hacia adelante
                    }
                }
            }
        }*/
        

    }
    public void ChangeState(IState newState)
    {
        var transitionParameters = fsm.CurrentState.Exit(newState);
        fsm.CurrentState = newState;
        fsm.CurrentState.Enter(fsm.CurrentState, transitionParameters);
    }
    public void DisableEnemy(){
        fsm.Active = false;
    }
    public Vector3 Position {
        get => transform.position;
        set => transform.position = value;
    }
    #region Checks
    public bool IsFacingDirection()
    {
        float currentAngle = transform.eulerAngles.z;
        return currentAngle > (direction - 90) && currentAngle < (direction + 90);
    }
    #endregion
    #region Collisions
    private void OnCollisionEnter2D(Collision2D other) {
        if(!other.gameObject.CompareTag("Missiles")){   //Ya la clase misil se encarga de manejar los daños cuando golpea, por lo que si no es el misil
            int damage = enemy.CollisionBehaviour(other.gameObject, this);  //Se encarga del comportamiento al colisionar
            TakeDamage(damage);     //Llama a la funcion que hace el daño
        }  
        ChangeState(idleState);
    }

    private void OnTriggerEnter2D(Collider2D other) {       //si el enemigo tiene un trigger, llama al comportamiento que se encarga del trigger enter
        enemy.TriggerBehaviour(other.gameObject);
        if(other.gameObject.CompareTag("Missiles"))
            ChangeState(idleState);
    }
    #endregion
    #region TakeDamage
    public void TakeDamage(float damage){       //Le quita la vida correspondiente al enemigo y si es menor a 0 llama a la funcion de muerte
        //Debug.Log(gameObject + " recibio "+ damage+ " de dano" );
        life -= damage;
        life = Mathf.Clamp(life, -100f, enemy.maxLife);
        view.TakeDamageView(life,enemy.maxLife);
        if(absorb && specialParticle != null) {specialParticle.SetActive(true);}
        if(life<= 0){
            Death(); 
        }
    }

    public void TakeDamageForExplosion(ExplosionsTypes type){       //Funcion que es llamada por una explosion para calcular el daño que recibe
        int damage = DamageTypes.Instance.explosionDictionary[type];  //Le manda el tipo de explosion a un LookUpTable y este le devuelve que daño hace
        TakeDamage(damage);
    }
    #endregion
    #region States
    private void Death(){       //Comportamiento de muerte
        GameObject explosion = enemy.DeathBehaviour();      //Llama al comportamiento que tiene que hacer al morir y devuelve una explosion
        explosion.transform.position = transform.position;      //la setea en la posicion del enemigo
        notifyKillCount?.Invoke(1);
        notifyScore?.Invoke(enemy.score);
        enemy.DropPowerUp(transform);       //llama a la funcion que se encarga de generar drops
        this.gameObject.SetActive(false);       //Desactiva este objeto
        
    }

    public void MoveForward() {
        if(normalDir){
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else{
            transform.Translate(-transform.right * speed * Time.deltaTime);
        }
        OnMove?.Invoke(this);
    }

    public void Rotate() {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, direction), rotationSpeed * Time.deltaTime);
        //Debug.Log(transform.rotation);
    }

    public void UpdateParticle(){
        if(specialParticle != null){        //si hay una particula es porque tiene un comportamiento especial por lo que llama al comportamiento
            enemy.ParticleBehaviour(specialParticle);
        }
    }
    #endregion

}
