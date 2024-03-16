using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ExplosionStrategy explosion;
    [SerializeField] public AnimEvents events;
    [SerializeField] public int creatorId;

    private void Start() {
        events.ADD_EVENT("dealDamage", DealDamage);
        events.ADD_EVENT("end", DisableObject);
        events.ADD_EVENT("explosion", ExplosionBehaviour);
        events.ADD_EVENT("implosion", ImplosionBehaviour);
    }

    private void DealDamage(){
        explosion.DealDamage(transform);
    }

    private void ExplosionBehaviour(){
        explosion.ExplosionBehaviour(transform);
    }
    private void ImplosionBehaviour(){
        explosion.ImplosionBehaviour(transform);
    }
    private void DisableObject(){
        this.gameObject.SetActive(false);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosion.radius);
    }
}
