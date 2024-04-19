using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Missile", menuName = "ScriptableObject/Missiles/Sticky", order = 2)]
public class StickyMissileBehaviour : MissileStrategy
///Este tipo de misil al entrar al collider de un enemigo, lo guarda, y luego se mueve a travez de el para que al salir se
///dirija devuelta en direccion del enemigo, pegandose a el y haciendole daño cada vez que pasa a travez de el.
///Si el enemigo muere en el proceso, sigue en la direccion que iba hasta golpear a otro
{
    [Header("Special Properties")]
    [SerializeField] private GameObject enemyPierced;   //El enemigo que golpeo
    [SerializeField] private float force;   //La fuerza con la que lo empuja
    private Vector2 direction;      //La direccion hacia donde se tiene que mover

    private void OnEnter(GameObject other, GameObject prefab){      //Guarda la posicion del enemigo y se mueve en esa direccion, mientras que lo empuja
        Rigidbody2D rb2D = other.GetComponent<Rigidbody2D>();
        if(rb2D != null){
            Vector2 direction = other.transform.position - prefab.transform.position;
            float distance = 1 + direction.magnitude;
            float finalForce = force / distance;
            rb2D.AddForce(direction * finalForce);
        }
    }

    //Detecta colisiones, si es con una pared, simula un rebote, si es un enemigo, llama a la funcion onEnter y le reduce la vida al misil
    public override int CollisionBehaviour(GameObject other, GameObject prefab){
        int layer = other.layer;
        int damage;
        switch (layer)
        {
            case 7:
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                Rigidbody2D rb2D = prefab.GetComponent<Rigidbody2D>();
                Vector2 bounceDirection = rb2D.velocity;
                bounceDirection.y = bounceDirection.y * -1;
                AudioManager.Instance.PlaySoundEffect(bounceEffect);
                rb2D.velocity = bounceDirection;
                return damage;
            case 8:
            case 9:
            case 10:
            case 11:
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                enemyPierced = other.gameObject;
                OnEnter(other.gameObject, prefab.gameObject);
                DealDamage(other, prefab);
                return damage;
            default:
                damage = 0;
                return damage;
        }
    }
    //mientras este dentro del enemigo y es el que entro, se mueve en la direccion correspondiente
    public override void SpecialBehaviourStay(GameObject other,GameObject prefab){
        if (other.gameObject == enemyPierced)
        {
            direction = ((Vector2)other.transform.position - (Vector2)prefab.transform.position).normalized;
        }
    }

    //Cuando sale del enemigo y es al que habia entrado, guarda la nueva posicion del enemigo para moverse en la nueva direccion
    public override void SpecialBehaviourExit(GameObject other,GameObject prefab){
        if (other.gameObject == enemyPierced)
        {
            enemyPierced = null;
            prefab.GetComponent<Rigidbody2D>().velocity = direction * 5;
            
        }
    }

    //Crea la explosion correspondiente al quedarse sin vida
    public override void ExplosionBehaviour(Transform origin){
        GameObject newExplosion = ExplosionPool.Instance.RequestExplosion(base.explosion);
        newExplosion.transform.position = origin.position;
        AudioManager.Instance.PlaySoundEffect(explosionEffect);
    }
}
