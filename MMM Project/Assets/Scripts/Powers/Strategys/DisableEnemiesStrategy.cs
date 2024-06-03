using UnityEngine;



[CreateAssetMenu(fileName = "New Power", menuName = "ScriptableObject/Power/Disable", order = 3)]
public class DisableEnemiesStrategy : PowerStrategy
{   
    /// <summary>
    /// Este tipo de poder Desactiva el movimiento de un enemigo
    /// </summary>
    private EnemyBehaviour enemy;   //El enemigo donde se presiona
    public Explosion hackExplosion;
    public override bool BehaviourStarted(){    //El comportamiento cuando se presiona
        // Convertir la posición del clic del ratón a un rayo en el mundo 2D
        Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero);
        
        // Verificar si el objeto colisionado es un enemigo
        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            // Si es un enemigo, activar el poder que se activa al hacer clic en un enemigo
            Activate(hit.collider.gameObject);
            return true;
        }
        else{
            //Debug.Log("Invalid Action");
            AudioManager.Instance.PlaySoundEffect(invalidEffect);
            return false;
        }
    }

    private void Activate(GameObject other){    //Deshabilita la capacidad de moverse del enemigo
        enemy = other.GetComponent<EnemyBehaviour>();
        enemy.DisableEnemy();
        Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CreateExplosion(origin);
        Debug.Log("Navigation Hack");
    }
    public override bool BehaviourPerformed(){  
        return true;
    }
    public override void BehaviourEnded(){
    }
    private void CreateExplosion(Vector2 origin){   //Crea la explosion correspondiente
        GameObject explosion = ExplosionPool.Instance.RequestExplosion(hackExplosion);
        explosion.transform.position = origin;
    }
}
