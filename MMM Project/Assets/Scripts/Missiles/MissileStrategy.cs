using UnityEngine;


public abstract class MissileStrategy : ScriptableObject        //Strategy para todos los tipos de missiles
{
    #region Variables
    [SerializeField]private float energyConsumption;    //La cantidad de energia que consume al lanzarse
    [SerializeField]private float maxLife;  //La vida interna del misil
    [SerializeField]private float minDamage;        //El da;o minimo
    [SerializeField]private float maxDamage;        //el da;o maximo
    [SerializeField]private float velocity;     //La velocidad con la que se mueve
    [SerializeField]private float minStability;     //La estabilidad minima
    [SerializeField]private float maxStability;     //La estabilidad maxima
    [SerializeField]private MissileBehaviour prefab;    //El prefab del misl
    [SerializeField]private Explosion explosion;        //La explosion que genera al destruirse
    #endregion
    #region Declaration
    public float EnergyConsumption{get{return energyConsumption;}}
    public float MaxLife{get{return maxLife;}}
    public float Velocity{get{return velocity;}}
    public float MinStability{get{return minStability;}}
    public float MaxStability{get{return maxStability;}}
    public Explosion NewExplosion{get{return explosion;}}
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
            if(enemy.Absorb){
                damage = damage * -1;
                prefab.SetActive(false);
            }
            enemy.TakeDamage(damage);
        }
    }
    #endregion
}

