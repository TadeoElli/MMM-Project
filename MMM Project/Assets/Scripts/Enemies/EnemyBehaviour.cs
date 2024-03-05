using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] public EnemyStrategy enemy;
    [SerializeField] private float life;
    private float rotationSpeed, speed;
    private float direction;
    [SerializeField] private bool canMove = true;
    [SerializeField] public bool dirRight = false;
    [SerializeField] public bool absorb = false;

    private float timer;


    private void OnEnable() {
        life = enemy.maxLife;
        rotationSpeed = enemy.rotationSpeed;
        speed = enemy.velocity;
        direction = dirRight ? 270:90;
    }

    void Update() {
        // Aplica la estrategia de movimiento actual
        //Debug.Log(transform.eulerAngles.z);
        if(canMove){
            if(GetComponent<Rigidbody2D>().velocity.magnitude < 0.2){
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
        int damage = enemy.CollisionBehaviour(other.gameObject, this);
        timer = 0;
        TakeDamage(damage);
        
    }

    public void TakeDamage(float damage){
        Debug.Log("Enemigo recibio "+ damage+ " de dano" );
        life -= damage;
        life = Mathf.Clamp(life, -100f, enemy.maxLife);
        if(life<= 0){
            Death(); 
        }
    }

    public void TakeDamageForExplosion(int creatorId){
        int damage = DamageTypes.Instance.explosionDictionary[creatorId];
        TakeDamage(damage);
    }

    private void Death(){
        GameObject explosion = ExplosionPool.Instance.RequestExplosion(enemy.explosion);
        explosion.GetComponent<Explosion>().creatorId =  enemy.id;
        explosion.transform.position = transform.position;
        this.gameObject.SetActive(false);
    }

    public void MoveForward() {
        if(dirRight){
            transform.Translate(-transform.right * speed * Time.deltaTime);
        }
        else{
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
    }

    private void Rotate() {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, direction), rotationSpeed * Time.deltaTime);
        //Debug.Log(transform.rotation);
    }


}
