using UnityEngine;


public abstract class EnemyStrategy : ScriptableObject        //Strategy para todos los tipos de missiles
{
    public float maxLife;
    public float rotationSpeed;
    public float collisionForce;
    public float velocity;
    public GameObject explosion;


    public abstract int CollisionBehaviour(GameObject other,EnemyBehaviour prefab);
    
    public void CollisionForce(GameObject other, EnemyBehaviour prefab){
        Rigidbody2D rb2D = other.GetComponent<Rigidbody2D>();

        Vector2 direction = other.transform.position - prefab.transform.position;
            
        rb2D.AddForce(direction.normalized * collisionForce, ForceMode2D.Force);
    }
    public abstract GameObject DeathBehaviour();
    public abstract void ParticleBehaviour(GameObject particle);
    public abstract void TriggerBehaviour(GameObject other);

    
}
