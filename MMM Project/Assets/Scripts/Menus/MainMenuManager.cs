using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    /// <summary>
    /// Esta clase se va a encargar de administrar el menu principal
    /// </summary>
    [SerializeField] private GameObject mainMenu, typesOfGamesMenu, settingsMenu, soundIconOn, soundIconOff, soundTextOn, soundTextOff;
    void Start()
    {
        mainMenu.SetActive(true);
        typesOfGamesMenu.SetActive(false);
    }

    public void ChangeToTypesOfGamesMenu(){
        mainMenu.SetActive(false);
        typesOfGamesMenu.SetActive(true);
    }
    public void ChangeToMainMenu(){
        mainMenu.SetActive(true);
        typesOfGamesMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }
    public void ChangeToSettingsMenu(){
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void SetSoundIcon(bool state){
        if(state){
            soundIconOff.SetActive(false);
            soundIconOn.SetActive(true);
            soundTextOff.SetActive(false);
            soundTextOn.SetActive(true);
        }else{
            soundIconOff.SetActive(true);
            soundIconOn.SetActive(false);
            soundTextOff.SetActive(true);
            soundTextOn.SetActive(false);
        }
    }
}
