using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Tower", menuName = "ScriptableObject/Tower/Acceleration", order = 2)]
public class AccelerationTowerBehaviour : TowerStrategy
{
    [SerializeField] private float attractionStrength;
    [SerializeField] private float repulsionRadius;
    [SerializeField] private float radius;
    [SerializeField] private float maxRotationSpeed;
    [SerializeField] private float repulsionStrength;
    [SerializeField] private float angularAcceleration;

    public override void CreateTower(Vector2 origin){
        GameObject tower = TowersPool.Instance.RequestTower(prefab);
        tower.transform.position = origin;
    }
    public override void SpecialBehaviour(GameObject prefab, GameObject other){
        if (other.CompareTag("Missiles"))
        {
            Rigidbody2D projectileRigidbody = other.GetComponent<Rigidbody2D>();
            MissileBehavior missile = other.GetComponent<MissileBehavior>();

            if (projectileRigidbody != null)
            {
                Vector2 localPosition = prefab.transform.InverseTransformPoint(other.transform.position);
                float distance = localPosition.magnitude;

                if (distance < radius && !missile.hasBeenAttracted)
                {
                    if (distance > repulsionRadius)
                    {
                        // Fuerza de atracción proporcional a la distancia
                        float attractionForce = attractionStrength / distance;

                        // Aplicar fuerza de atracción
                        projectileRigidbody.AddForce(localPosition.normalized * attractionForce, ForceMode2D.Force);
                    }
                    else
                    {
                        // Detener el proyectil cuando está dentro del radio de repulsión
                        projectileRigidbody.velocity = Vector2.zero;
                        missile.hasBeenAttracted = true;
                    }

                    // Determinar el sentido de rotación (horario o antihorario) según la posición local
                    float rotationDirection = (localPosition.x > 0) ? 1f : -1f;

                    // Rotación gradual alrededor de la torre
                    other.transform.RotateAround(prefab.transform.position, Vector3.forward, rotationDirection * missile.rotationSpeed * Time.deltaTime);

                    // Aumentar velocidad angular con el tiempo
                    missile.rotationSpeed = Mathf.Clamp(missile.rotationSpeed + angularAcceleration * Time.deltaTime, 0f, maxRotationSpeed);
                }
                else
                {
                    // El proyectil está fuera del rango de atracción
                    if (missile.hasBeenAttracted)
                    {
                        // Puedes reiniciar el estado del proyectil para que pueda ser atraído nuevamente
                        missile.hasBeenAttracted = false;
                        missile.rotationSpeed = 0f;
                    }

                    // Fuerza de repulsión proporcional a la distancia
                    float repulsionForce = repulsionStrength / distance;

                    // Aplicar fuerza de repulsión en la dirección opuesta a la posición local
                    projectileRigidbody.AddForce(-localPosition.normalized * repulsionForce, ForceMode2D.Force);
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
