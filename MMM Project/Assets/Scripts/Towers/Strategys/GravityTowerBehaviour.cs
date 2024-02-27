using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Tower", menuName = "ScriptableObject/Tower/Gravity", order = 0)]
public class GravityTowerBehaviour : TowerStrategy
{
    private GameObject enemy;
    [SerializeField] private float repulsionRadius;
    [SerializeField] private float repulsionStrength;
    [SerializeField] private float attractionStrength;

    public override void CreateTower(Vector2 origin){
        GameObject tower = TowersPool.Instance.RequestTower(prefab);
        tower.transform.position = origin;
    }
    public override void SpecialBehaviour(GameObject prefab){

        Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();

        if (enemyRigidbody != null)
        {
            Vector2 direction = prefab.transform.position - enemy.transform.position;
            float distance = direction.magnitude;

            if (distance > 0)
            {
                if(distance > repulsionRadius){
                    // Fuerza de atracción proporcional a la distancia
                    float attractionForce = attractionStrength / distance;

                    // Aplicar fuerza de atracción
                    enemyRigidbody.AddForce(direction.normalized * attractionForce, ForceMode2D.Force);
                }
                else{
                    // Fuerza de repulsión proporcional a la distancia
                    float repulsionForce = repulsionStrength / distance;

                    // Aplicar fuerza de repulsión
                    enemyRigidbody.AddForce(direction.normalized * repulsionForce, ForceMode2D.Force);
                }
            }
        }
    }

    public override void ColliderBehaviour(GameObject prefab, GameObject other){
        if(other.CompareTag("Enemy")){
            enemy = other;
        }
    }

    
}
