using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Tower", menuName = "ScriptableObject/Tower/Acceleration", order = 2)]
public class AccelerationTowerBehaviour : TowerStrategy
{
    [SerializeField] private float attractionStrength;

    [SerializeField] private float repulsionStrength;
    [SerializeField] private float repulsionRadius;
    [SerializeField] private float radius;
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

                if (distance < radius)
                {
                    if(distance > repulsionRadius && !missile.hasBeenAtracted){
                        // Fuerza de atracción proporcional a la distancia
                        float attractionForce = attractionStrength / distance;

                        // Aplicar fuerza de atracción
                        projectileRigidbody.AddForce(direction.normalized * attractionForce, ForceMode2D.Force);
                    }
                    else if(distance < repulsionRadius && !missile.hasBeenAtracted){
                        projectileRigidbody.velocity = Vector2.zero;
                        other.transform.position = prefab.transform.position;
                        missile.hasBeenAtracted = true;
                        projectileRigidbody.AddForce(direction.normalized * repulsionStrength, ForceMode2D.Force);
                        // Determinar el sentido de rotación (horario o antihorario) según la posición local
                        missile.rotationDirection = (direction.y > 0) ? 1f : -1f;
                    }
                    else{
                        if(distance < radius - 0.1f){
                            // Aplicar fuerza de repulsion
                            other.transform.RotateAround(prefab.transform.position, Vector3.forward, (missile.rotationSpeed * missile.rotationDirection) * Time.deltaTime);
                            // Aumentar velocidad angular con el tiempo
                            missile.rotationSpeed = missile.rotationSpeed + angularAcceleration * Time.deltaTime;
                            missile.rotationSpeed = Mathf.Clamp(missile.rotationSpeed, 100f, 700f);
                            float repulsionForce = repulsionStrength * (distance / radius);

                            projectileRigidbody.AddForce(direction.normalized * repulsionForce, ForceMode2D.Force);
                        }
                    }
                }
                else if(distance >= radius && missile.hasBeenAtracted){
                    if(direction.x < 0){
                        Debug.Log("adelante");
                        projectileRigidbody.AddForce(-direction.normalized * repulsionStrength * 10f, ForceMode2D.Force);
                    }
                    else{
                        Debug.Log("atras");
                        projectileRigidbody.AddForce(direction.normalized * repulsionStrength * 100f, ForceMode2D.Force);
                    }
                    missile.hasBeenAtracted = false;
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

    public override void DestroyTower(GameObject prefab){
        Collider2D[] objetos = Physics2D.OverlapCircleAll(prefab.transform.position, radius);

        foreach (Collider2D collisions in objetos){
            if(collisions.CompareTag("Missiles")){
                Rigidbody2D rb2D = collisions.GetComponent<Rigidbody2D>();
                if(rb2D != null){
                    Vector2 direction = collisions.transform.position - prefab.transform.position;
                    float distance = 1 + direction.magnitude;
                    float finalForce = (repulsionStrength * 100f) / distance;
                    rb2D.AddForce(direction * finalForce);
                }
            }
        }
    }
    
}
