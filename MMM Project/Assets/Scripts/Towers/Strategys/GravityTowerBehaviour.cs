using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Tower", menuName = "ScriptableObject/Tower/Gravity", order = 0)]
public class GravityTowerBehaviour : TowerStrategy
{
    /// <summary>
    /// Esta torre tendra un collider que cuando un enemigo entre, lo atraera y cuando este muy cerca lo alejara, funcionando como un magnetismo
    /// Que hara que todos los enemigos choquen entre si hasta que salgan del radio de efecto
    /// </summary>
    [Header("Special Properties")]
    [SerializeField] private float repulsionRadius; //El radio de repulsion
    [SerializeField] private float repulsionStrength;   //La fuerza de repulsion
    [SerializeField] private float attractionStrength;  //la fuerza de atraccion
    [SerializeField] private float radius;  //El radio de atraccion

    //Si un enemigo entra en el radio, toma su rigidbody y su posicion y calcula la distancia
    public override void SpecialBehaviour(GameObject prefab, GameObject other){

        if(other.CompareTag("Enemy")){
            Rigidbody2D enemyRigidbody = other.GetComponent<Rigidbody2D>();

            if (enemyRigidbody != null)
            {
                Vector2 direction = prefab.transform.position - other.transform.position;
                float distance = direction.magnitude;

                if (distance > 0)   //Si es mayer a 0
                {
                    if(distance > repulsionRadius){ //y mayer al radio de repulsion
                        // Fuerza de atracción proporcional a la distancia
                        float attractionForce = attractionStrength / distance;

                        // Aplicar fuerza de atracción
                        enemyRigidbody.AddForce(direction.normalized * attractionForce, ForceMode2D.Force);
                    }
                    else{//si no
                        // Fuerza de repulsión proporcional a la distancia
                        float repulsionForce = repulsionStrength / distance;

                        // Aplicar fuerza de repulsión
                        enemyRigidbody.AddForce(direction.normalized * repulsionForce, ForceMode2D.Force);
                    }
                }
            }
        }
    }

    public override bool ColliderBehaviour(GameObject prefab, GameObject other){
        if(other.CompareTag("Enemy")){
            return true;
        }
        return false;
        
    }
    //El comportamiento de cuando se destruye la torre, toma todos los componentes Enemigos dentro del radio y los envia en la direccion contraria
    public override void DestroyTower(GameObject prefab){
        Collider2D[] objetos = Physics2D.OverlapCircleAll(prefab.transform.position, radius);

        foreach (Collider2D collisions in objetos){
            if(collisions.CompareTag("Enemy")){
                Rigidbody2D rb2D = collisions.GetComponent<Rigidbody2D>();
                if(rb2D != null){
                    Vector2 direction = collisions.transform.position - prefab.transform.position;
                    float distance = 1 + direction.magnitude;
                    float finalForce = repulsionStrength / distance;
                    rb2D.AddForce(direction * finalForce);
                }
            }
        }
    }

    
}
