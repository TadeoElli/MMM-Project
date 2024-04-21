using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemies/Repulse", order = 1)]
public class RepulseEnemyStrategy : EnemyStrategy
///Este enemigo creara una onda expansiva cada tantos segundos y si esta onda se produce mientras hay un misil dentro
///lo rechazara, haciendo que sea mas dificil golpearlo
{
    [SerializeField] private float cooldown;        //Cooldown para que active la onda expansiva
    [SerializeField] private GameObject particle;       //Particula de la expansion (solo visual)
    private float timer = 0;
    public override int CollisionBehaviour(GameObject other, EnemyBehaviour prefab){        //Comportamiento de collisiones            
        int layer = other.layer;
        int damage;
        switch (layer)  //Dependiendo del layer con el choco llama a un LookUpTable de tipos de daño de colisiones
        {
            case 7:
                damage = DamageTypes.Instance.collisionEnemiesDictionary[layer];
                if(prefab.GetComponentInChildren<SpriteRenderer>().isVisible)
                    AudioManager.Instance.PlaySoundEffect(bounceClip);
                return damage;
            case 8:
            case 9:
            case 10:
                damage = DamageTypes.Instance.collisionEnemiesDictionary[layer];
                CollisionForce(other, prefab);       //Llama a la funcion para empujar al otro enemigo
                if(prefab.GetComponentInChildren<SpriteRenderer>().isVisible)
                    AudioManager.Instance.PlaySoundEffect(bounceClip);
                return damage;
            default:
                damage = 0;
                return damage;
        }
    }

    public override GameObject DeathBehaviour(){    //Crea la explosion de muerte
        GameObject explosion = ExplosionPool.Instance.RequestExplosion(base.explosion);
        return explosion;
    }
    public override void ParticleBehaviour(GameObject specialParticle){     //Comportamiento especial de las particulas
        if(timer > cooldown){
            timer = 0;
            specialParticle.SetActive(true);
        }
        else{
            timer = timer + 1 * Time.deltaTime;
        }
    }
    public override void TriggerBehaviour(GameObject other){

    }
    
}
