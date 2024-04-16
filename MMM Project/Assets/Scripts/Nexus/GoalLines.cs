using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalLines : MonoBehaviour
{
    /// <summary>
    /// Esta clase se encarga de administrar las lineas de meta de los niveles para determinar cuando un enemigo pasa esa linea y que se reste la cantidad
    /// de vidas, ademas de que guarda ese enemigo en una lista para que si la vuelve a cruzar porque se choco con algo, no cuente para restar una vida
    /// </summary>
    // Start is called before the first frame update
    List<GameObject> enemyList; //La lista de enemigos que pasaron la linea
    [SerializeField] private bool isOnLeft; //Si la linea se encuentra del lado izquierdo del mapa entonces esta en true
    [SerializeField] private UnityEvent reduceLives;
    void Start()
    {
        enemyList = new List<GameObject>();
        
    }

//Cuando sale de la linea, si es un enemigo que no estaba en la lista, y la direccion en la que se dirigia es la correcta, reduce la cantidad de vidas
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Enemy") && !enemyList.Contains(other.gameObject)){
            if((isOnLeft && other.GetComponent<EnemyBehaviour>().normalDir) || !isOnLeft && !other.GetComponent<EnemyBehaviour>().normalDir){
                enemyList.Add(other.gameObject);
                reduceLives?.Invoke();
                other.gameObject.SetActive(false);
            }
        }

    }
}
