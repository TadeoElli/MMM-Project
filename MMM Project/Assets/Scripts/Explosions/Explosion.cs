using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ExplosionStrategy explosion;   //El strategy de la explosion
    [SerializeField] private AnimEvents events;     //El controlador de eventos de las animaciones

    private void Start() {  
        events.ADD_EVENT("dealDamage", DealDamage);     
        events.ADD_EVENT("end", DisableObject);
        events.ADD_EVENT("explosion", ExplosionBehaviour);
        events.ADD_EVENT("implosion", ImplosionBehaviour);
    }

    private void DealDamage(){  //Funcion para hacer daño
        explosion.DealDamage(transform);
    }

    private void ExplosionBehaviour(){      //Comportamiento de explosion (empuje)
        explosion.ExplosionBehaviour(transform);
    }
    private void ImplosionBehaviour(){   //Comportamiento de implosion (atraccion)
        explosion.ImplosionBehaviour(transform);
    }
    private void DisableObject(){    //Desactiva el objeto
        this.gameObject.SetActive(false);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosion.radius);
    }
}
