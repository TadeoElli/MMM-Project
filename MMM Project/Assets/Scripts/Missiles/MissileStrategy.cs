using UnityEngine;

public abstract class MissileStrategy : ScriptableObject
{
    #region Variables
    public float energyConsumption;    // La cantidad de energía que consume al lanzarse
    public float maxLife;              // La vida interna del misil
    public float minDamage;            // El daño mínimo
    public float maxDamage;            // El daño máximo
    public float velocity;             // La velocidad con la que se mueve
    public float minStability;         // La estabilidad mínima
    public float maxStability;         // La estabilidad máxima
    public MissileBehaviour prefab;    // El prefab del misil
    public Explosion explosion;        // La explosión que genera al destruirse
    [Header("Feedback")]
    public Color color;
    public Sprite sprite;
    public Texture texture;
    public AudioClip launchEffect, bounceEffect, bounceEnemyEffect;
    #endregion

    #region Behaviours
    public GameObject CreateMissile(Transform origin)
    {
        GameObject missile = MissilePool.Instance.RequestMissile(prefab);
        missile.transform.position = origin.position;
        return missile;
    }

    // Comportamientos especiales
    public virtual void SpecialBehaviourStay(GameObject other, GameObject prefab){
        //Comportamiento especial mientras esta dentro
    }
    public virtual void SpecialBehaviourExit(GameObject other, GameObject prefab){
        //Comportamiento especial cuando sale
    }

    // Comportamiento en colisión
    public abstract int CollisionBehaviour(GameObject other, GameObject prefab);

    // Comportamiento de la explosión
    public virtual void ExplosionBehaviour(Transform origin){
        GameObject newExplosion = ExplosionPool.Instance.RequestExplosion(explosion);
        newExplosion.transform.position = origin.position;
    }

    // Inflicción de daño
    public void DealDamage(GameObject other, GameObject prefab)
    {
        float damage = Random.Range(minDamage, maxDamage);
        if (other.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemy))
        {
            if (enemy.absorb)
            {
                damage *= -1; // Invierte el daño para curar al enemigo
                prefab.SetActive(false);
            }
            enemy.TakeDamage(damage);
        }
    }
    #endregion
}

