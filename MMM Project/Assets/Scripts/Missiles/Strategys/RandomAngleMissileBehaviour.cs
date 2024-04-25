using UnityEngine;


[CreateAssetMenu(fileName = "New Missile", menuName = "ScriptableObject/Missiles/Angle", order = 1)]
public class RandomAngleMissileBehaviour : MissileStrategy
///este tipo de misil crea una direccion aleatoria cada vez que rebota en un enemigo y se mueve en esa direccion
{

    //Esta funcion se encarga de que cuando entre en contracto con un collider, genere una nueva direccion
    //dentro de un rango de 180 grados y se manda en la nueva direccion simulando un rebote aleatorio

    // Rango de ángulos para el rebote aleatorio
    private const float MinRandomAngle = -Mathf.PI / 2f;
    private const float MaxRandomAngle = Mathf.PI / 2f;

    // Función para generar una nueva dirección aleatoria dentro del rango especificado
    private Vector2 GenerateRandomDirection()
    {
        float newAngle = Random.Range(MinRandomAngle, MaxRandomAngle);
        return new Vector2(Mathf.Cos(newAngle), Mathf.Sin(newAngle));
    }

    private void OnEnter(GameObject prefab){    
        Rigidbody2D rigidbody2D = prefab.GetComponent<Rigidbody2D>();
        Vector2 newDirection = GenerateRandomDirection();
        //Debug.Log(newDirection);
        rigidbody2D.velocity = newDirection * rigidbody2D.velocity.magnitude;
    }
    //El comportamiento de colision
    public override int CollisionBehaviour(GameObject other, GameObject prefab){
        int layer = other.layer;
        int damage = 0;
        switch (layer)
        {
            case 7:
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                AudioManager.Instance.PlaySoundEffect(bounceEffect);
                return damage;
            case 8:
            case 9:
            case 10:
            case 11:
                damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
                OnEnter(prefab);
                DealDamage(other, prefab);
                if(other.GetComponentInChildren<SpriteRenderer>().isVisible)
                    AudioManager.Instance.PlaySoundEffect(bounceEnemyEffect);
                return damage;
            default:
                return damage;
        }
    }


}
