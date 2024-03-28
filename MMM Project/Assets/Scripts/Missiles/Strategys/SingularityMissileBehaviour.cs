using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Missile", menuName = "ScriptableObject/Missiles/Singularity", order = 3)]
public class SingularityMissileBehaviour : MissileStrategy
///Este tipo de misil funciona como un agujero negro, atrayendo a todos los enemigos a los que entra en contacto hacia el centro
///Y haciendo que se choquen entre si, por eso no hace daño, el daño se produce por los choques
{
    [SerializeField] private float force;       //La fuerza con la que atrae

    //si collisiona con una pared, simula el movimiento de rebote y pierde algo de vida
    public override int CollisionBehaviour(GameObject other, GameObject prefab){
        int layer = other.layer;
        int damage;
        if(layer == 7){
            damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
            Rigidbody2D rb2D = prefab.GetComponent<Rigidbody2D>();
            Vector2 bounceDirection = rb2D.velocity;
            bounceDirection.y = bounceDirection.y * -1;

            rb2D.velocity = bounceDirection;
            return damage;
        }
        else{
            return 0;
        }
    }

    //Mientras este en contacto con un enemigo, genera la direccion a la que se tiene que mover el enemigo y lo atrae constantemente
    public override void SpecialBehaviourStay(GameObject other,GameObject prefab){
        Vector2 direction = (prefab.transform.position - other.transform.position).normalized;
        other.transform.position = Vector2.Lerp(other.transform.position, prefab.transform.position, force * Time.deltaTime);
    }
    public override void SpecialBehaviourExit(GameObject other,GameObject prefab){
    }

    //Crea la explosion correspondiente cuando se queda sin vida
    public override void ExplosionBehaviour(Transform origin){
        GameObject newExplosion = ExplosionPool.Instance.RequestExplosion(base.NewExplosion);
        newExplosion.transform.position = origin.position;
    }
}
