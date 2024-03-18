using UnityEngine;


public abstract class EnemyStrategy : ScriptableObject        //Strategy para todos los tipos de missiles
{
    public float maxLife;
    public float rotationSpeed;
    public float collisionForce;
    public float velocity;
    public GameObject prefab;
    public GameObject explosion;

    public GameObject CreateEnemy(Transform origin){
        GameObject enemy = MissilePool.Instance.RequestMissile(prefab);
        enemy.transform.position = origin.position;
        return  enemy;
    }
    public abstract int CollisionBehaviour(GameObject other,EnemyBehaviour prefab);
    
    public void CollisionForce(GameObject other, EnemyBehaviour prefab){
        Rigidbody2D rb2D = other.GetComponent<Rigidbody2D>();

        Vector2 direction = other.transform.position - prefab.transform.position;
            
        rb2D.AddForce(direction.normalized * collisionForce, ForceMode2D.Force);
    }
    public abstract GameObject DeathBehaviour();
    
}
