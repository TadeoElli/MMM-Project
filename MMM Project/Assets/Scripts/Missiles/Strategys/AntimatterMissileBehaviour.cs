using UnityEngine;


[CreateAssetMenu(fileName = "New Missile", menuName = "ScriptableObject/Missiles/Antimatter", order = 4)]
public class AntimatterMissileBehaviour : MissileStrategy
//Este tipo de misil se activara con un power up y lo que hara sera que cuando choque con un enemigo explote
{

    //El comportamiento de cuando colisiona
    public override int CollisionBehaviour(GameObject other, GameObject prefab){
        MissileBehaviour missileBehaviour = prefab.GetComponent<MissileBehaviour>();
            
        int layer = other.layer;
        int damage = 0;
        switch (layer)
        {
            case 7:

                Rigidbody2D rb2D = prefab.GetComponent<Rigidbody2D>();
                Vector2 bounceDirection = rb2D.velocity;
                bounceDirection.y = bounceDirection.y * -1;
                AudioManager.Instance.PlaySoundEffect(bounceEffect);
                rb2D.velocity = bounceDirection;
                return damage;
            case 8:
            case 9:
            case 10:
            case 11:    //Recibe 2 veces daño por el "oneChance" que hace que despues de que se quede  con 0 de vida le 
                //permite aguantar un rebote mas
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                missileBehaviour.TakeDamage(10);
                missileBehaviour.TakeDamage(10);
                return damage;
            default:
                return damage;
        }
    }

}
