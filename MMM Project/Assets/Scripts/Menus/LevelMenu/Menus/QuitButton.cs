using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuitButton : MonoBehaviour
{
    /// <summary>
    /// Esta clase se va a encargar de llamar a la funcion Quit
    /// </summary>

    public void QuitGame(){
        GameManager.Instance.QuitGame();
    }
}