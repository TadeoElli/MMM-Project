using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMissiles : MonoBehaviour
{
    /// <summary>
    /// Esta clase es para disparar a los misiles del menu
    /// </summary>

    [SerializeField] private List<GameObject> prefabs;   //lista de los objetos a disparar
    [SerializeField] private float force;   //Con que fuerza van a ser disparados
    void Start()
    {
        foreach (var obj in prefabs)
        {
            Rigidbody2D rb2D = obj.GetComponent<Rigidbody2D>();
            if(rb2D != null){
                Vector2 direction = new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f));
                rb2D.AddForce(direction * force);
            } 
        }
    }


}
