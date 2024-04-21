using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMainMenu : MonoBehaviour
{
    /// <summary>
    /// Esta clase es para que se muestre el cursor en el menu principal
    /// </summary>
    Vector2 currentPosition;    //Donde se guarda la posicion del mouse

    void Start()
    {
        Cursor.visible = false; //Desactivo el mouse
    }

    // Update is called once per frame
    void Update()   
    {
        currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = currentPosition;
    }
 
}
