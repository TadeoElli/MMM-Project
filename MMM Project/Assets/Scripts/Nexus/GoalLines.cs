using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GoalLines : MonoBehaviour
{
    /// <summary>
    /// Esta clase se encarga de administrar las lineas de meta de los niveles para determinar cuando un enemigo pasa esa linea y que se reste la cantidad
    /// de vidas
    /// </summary>
    // Start is called before the first frame update
    [SerializeField] private bool isOnLeft; //Si la linea se encuentra del lado izquierdo del mapa entonces esta en true
    [SerializeField] private UnityEvent reduceLives;

//Cuando sale de la linea, si es un enemigo que no estaba en la lista, y la direccion en la que se dirigia es la correcta, reduce la cantidad de vidas
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Enemy")){
            if((isOnLeft && other.GetComponent<EnemyBehaviour>().normalDir) || !isOnLeft && !other.GetComponent<EnemyBehaviour>().normalDir){
                reduceLives?.Invoke();
                other.gameObject.SetActive(false);
            }
        }

    }
}
