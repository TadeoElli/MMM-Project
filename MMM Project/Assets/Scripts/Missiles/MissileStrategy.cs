using UnityEngine;


public abstract class MissileStrategy : ScriptableObject        //Strategy para todos los tipos de missiles
{
    #region Variables
    public float energyConsumption;    //La cantidad de energia que consume al lanzarse
    public float maxLife;  //La vida interna del misil
    public float minDamage;        //El da;o minimo
    public float maxDamage;        //el da;o maximo
    public float velocity;     //La velocidad con la que se mueve
    public float minStability;     //La estabilidad minima
    public float maxStability;     //La estabilidad maxima
    public MissileBehaviour prefab;    //El prefab del misl
    public Explosion explosion;        //La explosion que genera al destruirse
    [Header("Feedback")]
    public Color color;
    public Sprite sprite;
    public Texture texture;
    #endregion


    #region Behaviours
    public GameObject CreateMissile(Transform origin){      //Crea el misil y lo retorna
        GameObject missile = MissilePool.Instance.RequestMissile(prefab);
        missile.transform.position = origin.position;
        return  missile;
    } 
    //Comportamientos
    public abstract void SpecialBehaviourStay(GameObject other,GameObject prefab);  
    public abstract void SpecialBehaviourExit(GameObject other,GameObject prefab); 
    public abstract int CollisionBehaviour(GameObject other, GameObject prefab);
    public abstract void ExplosionBehaviour(Transform origin);
    
    public void DealDamage(GameObject other, GameObject prefab){    //Toma el otro objeto con el que colisiono y si es un enemigo le inflije daño
        float damage = Random.Range(minDamage,maxDamage);
        if (other.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemy))
        {   //Si el enemigo tiene la capacidad de absorver, invierto el daño para curarlo y luego desactivo el misil
            if(enemy.absorb){
                damage = damage * -1;
                prefab.SetActive(false);
            }
            enemy.TakeDamage(damage);
        }
    }
    #endregion
}

