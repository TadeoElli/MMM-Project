using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Tower", menuName = "ScriptableObject/Tower/Acceleration", order = 2)]
public class AccelerationTowerBehaviour : TowerStrategy
{
    [SerializeField] private float attractionStrength;
    [SerializeField] private float repulsionRadius;
    [SerializeField] private float radius;

    [SerializeField] private float repulsionStrength;
    [SerializeField] private float angularAcceleration;

    public override void CreateTower(Vector2 origin){
        GameObject tower = TowersPool.Instance.RequestTower(prefab);
        tower.transform.position = origin;
    }
    public override void SpecialBehaviour(GameObject prefab, GameObject other){
        if(other.CompareTag("Missiles")){
            Rigidbody2D projectileRigidbody = other.GetComponent<Rigidbody2D>();
            MissileBehavior missile = other.GetComponent<MissileBehavior>();

            if (projectileRigidbody != null)
            {
                Vector2 direction = prefab.transform.position - other.transform.position;
                float distance = direction.magnitude;

                if (distance < radius && !missile.hasBeenAtracted)
                {
                    if(distance > repulsionRadius && !missile.hasBeenAtracted){
                        // Fuerza de atracción proporcional a la distancia
                        float attractionForce = attractionStrength / distance;

                        // Aplicar fuerza de atracción
                        projectileRigidbody.AddForce(direction.normalized * attractionForce, ForceMode2D.Force);
                    }
                    else if(distance < repulsionRadius && !missile.hasBeenAtracted){
                        projectileRigidbody.velocity = Vector2.zero;
                        missile.hasBeenAtracted = true;
                    }
                    else{
                        // Fuerza de repulsión proporcional a la distancia
                        float repulsionForce = repulsionStrength / distance;

                        // Aplicar fuerza de repulsión
                        projectileRigidbody.AddForce(direction.normalized * repulsionForce, ForceMode2D.Force);
                        // Rotación gradual alrededor de la torre
                        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                        other.transform.RotateAround(prefab.transform.position, Vector3.forward, missile.rotationSpeed * Time.deltaTime);

                        // Aumentar velocidad angular con el tiempo
                        missile.rotationSpeed = missile.rotationSpeed + angularAcceleration * Time.deltaTime;
                    }
                }
                else if(distance > radius && missile.hasBeenAtracted){
                    missile.hasBeenAtracted = false;
                    //missile.rotationSpeed = 100f;
                    projectileRigidbody.AddForce(direction.normalized * (repulsionStrength * 100), ForceMode2D.Force);
                }
                
            }

        }
    }

    public override bool ColliderBehaviour(GameObject prefab, GameObject other){
        if(other.CompareTag("Missiles")){
            return true;
        }
        return false;
    }

    
}
