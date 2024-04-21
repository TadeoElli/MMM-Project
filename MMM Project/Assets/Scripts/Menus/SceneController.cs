using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Scenes scene;
    [SerializeField] private AudioClip sceneMusic;

    public void ChangeScene()
    {
        GameManager.Instance.SetGameMode(scene);
        string sceneName = scene.ToString(); // Convierte el valor del enum a una cadena (string).
        AudioManager.Instance.PlayMusic(sceneMusic);
        SceneManager.LoadScene(sceneName);
    }  

    

    

}

