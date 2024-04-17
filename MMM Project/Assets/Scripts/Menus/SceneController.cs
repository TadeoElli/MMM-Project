using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Scenes scene;

    public void ChangeScene()
    {
        string sceneName = scene.ToString(); // Convierte el valor del enum a una cadena (string).
        SceneManager.LoadScene(sceneName);
    }  
    

    public enum Scenes
    {
        MainMenu,
        Scenario,
        Survival,
        SurvivalExtreme,
        SurvivalUnlimited,
        Countdown,
        Armageddon,
        Crossfire
    }

}

