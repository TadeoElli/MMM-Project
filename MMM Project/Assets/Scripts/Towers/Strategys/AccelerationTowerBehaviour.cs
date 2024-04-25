using UnityEngine;
using System.Linq;


[CreateAssetMenu(fileName = "New Tower", menuName = "ScriptableObject/Tower/Acceleration", order = 2)]
public class AccelerationTowerBehaviour : TowerStrategy
{
    /// <summary>
    /// Este tipo de torre toma los misiles entrantes y los hace orbitar al rededor, aumentando su velocidad, y cuando lleguen al radio maximo los
    /// expulsa violentamente
    /// </summary>
    [Header("Special Properties")]
    [SerializeField] private float attractionStrength;  //la fuerza de atraccion
    [SerializeField] private float repulsionStrength;   //la fuersa de repulsion
    [SerializeField] private float repulsionRadius; //el radio de repulsion
    [SerializeField] private float radius;  //el radio de atraccion
    [SerializeField] private float angularAcceleration; //la aceleracion angular

    //El comportamiento de cuando tiene un misil dentro del collider
    public override void SpecialBehaviour(GameObject prefab, GameObject other){
        if(other.CompareTag("Missiles")){
            Rigidbody2D projectileRigidbody = other.GetComponent<Rigidbody2D>();
            MissileBehaviour missile = other.GetComponent<MissileBehaviour>();
            if (projectileRigidbody != null)
            {
                Vector2 direction = prefab.transform.position - other.transform.position;
                float distance = direction.magnitude;

                if (distance < radius)  //Calcula la distancia y si esta dentro del radio
                {
                    if(distance > repulsionRadius && !missile.hasBeenAtracted){ //Y es mayor al radio de repulsion y todavia no fue atraido
                        // Fuerza de atracción proporcional a la distancia
                        float attractionForce = attractionStrength / distance;

                        // Aplicar fuerza de atracción
                        projectileRigidbody.AddForce(direction.normalized * attractionForce, ForceMode2D.Force);
                    }
                    else if(distance < repulsionRadius && !missile.hasBeenAtracted){    //Si es menor al radio de repulsion y todavia no fue atraido
                        projectileRigidbody.velocity = Vector2.zero;    //Lo coloco en el centro de la torre, establezco que ya fue atraido y le empiezo a agregar fuerza
                        other.transform.position = prefab.transform.position;
                        missile.hasBeenAtracted = true;
                        projectileRigidbody.AddForce(direction.normalized * repulsionStrength, ForceMode2D.Force);
                        // Determinar el sentido de rotación (horario o antihorario) según la posición local
                        missile.RotationDirection = (direction.y > 0) ? 1f : -1f;
                    }
                    else{   //si ya fue atraido
                        if(distance < radius - 0.1f){   //Y esta dentro del radio
                            // Aplicar fuerza de repulsion
                            other.transform.RotateAround(prefab.transform.position, Vector3.forward, (missile.RotationSpeed * missile.RotationDirection) * Time.deltaTime);
                            // Aumentar velocidad angular con el tiempo
                            missile.RotationSpeed = missile.RotationSpeed + angularAcceleration * Time.deltaTime;
                            //missile.rotationSpeed = Mathf.Clamp(missile.rotationSpeed, 100f, 700f);
                            float repulsionForce = repulsionStrength * (distance / radius);

                            projectileRigidbody.AddForce(direction.normalized * repulsionForce, ForceMode2D.Force);
                        }
                    }
                }
                else if(distance >= radius && missile.hasBeenAtracted){ //Si esta fuera del radio y ya fue atraido
                    projectileRigidbody.AddForce((direction.x < 0 ? -direction.normalized : direction.normalized) * repulsionStrength * 10f, ForceMode2D.Force);
                    missile.hasBeenAtracted = false;    //Restablece el flag para que pueda ser atraido devuelta 
                    missile.RotationSpeed = missile.RotationSpeed / 2;
                }
                
            }

        }
    }

    public override bool ColliderBehaviour(GameObject prefab, GameObject other){
        return other.CompareTag("Missiles");
    }

    //El comportamiento de cuando se destruye la torre, toma todos los componentes misiles dentro del radio y los envia en la direccion contraria
    public override void DestroyTower(GameObject prefab){
        Collider2D[] objects = Physics2D.OverlapCircleAll(prefab.transform.position, radius);
        objects.Where(collision => collision.CompareTag("Missiles"))
               .ToList()
               .ForEach(collision =>
               {
                   Rigidbody2D rb2D = collision.GetComponent<Rigidbody2D>();
                   if (rb2D != null)
                   {
                       Vector2 direction = collision.transform.position - prefab.transform.position;
                       float distance = 1 + direction.magnitude;
                       float finalForce = (repulsionStrength * 100f) / distance;
                       rb2D.AddForce(direction * finalForce);
                   }
               });
    }
    
}
