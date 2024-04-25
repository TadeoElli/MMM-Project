using UnityEngine;


[CreateAssetMenu(fileName = "New Missile", menuName = "ScriptableObject/Missiles/Basic", order = 0)]
public class BasicMissilesStrategy : MissileStrategy
///Este tipo de misiles no tienen ningun funcionamiento especial, sin embargo pueden variar en daño, velocidad
///el tipo de material para manejar los rebotes o la masa
{
    //El comportamiento de colisiones
    public override int CollisionBehaviour(GameObject other, GameObject prefab){
        int layer = other.layer;
        int damage = 0;
        
        if (layer == 7 || (layer >= 8 && layer <= 11))
        {
            damage = DamageTypes.Instance.collisionMissilesDictionary[layer];
            if(layer == 7)
                AudioManager.Instance.PlaySoundEffect(bounceEffect);
            else
            {
                DealDamage(other, prefab);
                if(other.GetComponentInChildren<SpriteRenderer>().isVisible)
                    AudioManager.Instance.PlaySoundEffect(bounceEnemyEffect);
            }
        }
        return damage;
    }

}
