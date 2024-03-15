using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ExplosionStrategy explosion;
    [SerializeField] public AnimEvents events;
    [SerializeField] public int creatorId;

    private void Start() {
        events.ADD_EVENT("dealDamage", DealDamage);
        events.ADD_EVENT("end", DisableObject);
        events.ADD_EVENT("specialBehaviour", SpecialBehaviour);
    }

    private void DealDamage(){
        explosion.DealDamage(transform);
    }

    private void SpecialBehaviour(){
        explosion.SpecialBehaviour(transform);
    }
    private void DisableObject(){
        this.gameObject.SetActive(false);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosion.radius);
    }
}
