using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMovement : MonoBehaviour
{
    /// <summary>
    /// Esta clase se encarga de mover el background del menu segun el mouse para que se sienta mas vivo el menu
    /// </summary>
    float mousePosX;
    float mousePosY;
    [SerializeField] float movementQuantity;


    // Update is called once per frame
    void Update()
    {
        mousePosX = Input.mousePosition.x;
        mousePosY = Input.mousePosition.y;

        this.GetComponent<RectTransform>().position = new Vector2(
            (mousePosX / Screen.width) * movementQuantity + (Screen.width / 2),
            (mousePosY / Screen.height) * movementQuantity + (Screen.height / 2)
        );
    }
}
