using UnityEngine;


public abstract class EnemyStrategy : ScriptableObject        //Strategy para todos los tipos de missiles
{
    public int id;
    public float maxLife;
    public float rotationSpeed;
    public float velocity;
    public GameObject prefab;
    public GameObject explosion;

    public GameObject CreateEnemy(Transform origin){
        GameObject enemy = MissilePool.Instance.RequestMissile(prefab);
        enemy.transform.position = origin.position;
        return  enemy;
    }
    public abstract int CollisionBehaviour(GameObject other,EnemyBehaviour prefab);
    
}
