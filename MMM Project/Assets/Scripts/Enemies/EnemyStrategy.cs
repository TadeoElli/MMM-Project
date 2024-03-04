using UnityEngine;


public abstract class EnemyStrategy : ScriptableObject        //Strategy para todos los tipos de missiles
{
    public int id;
    public float maxLife;
    public float damageOnCollision;
    public float rotationSpeed;
    public float velocity;
    public GameObject prefab;
    public GameObject explosion;

    public abstract GameObject CreateEnemy(Transform origin); 
    public abstract int CollisionBehaviour(GameObject other,GameObject prefab);
    
}
