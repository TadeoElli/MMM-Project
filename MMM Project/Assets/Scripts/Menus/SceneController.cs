using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
        public enum EscenasEnum
    {
        MainMenu,
        Survival,
    }
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }  
    

    
}

