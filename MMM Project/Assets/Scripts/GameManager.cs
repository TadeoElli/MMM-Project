using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameModes gameModes;
    [SerializeField] private float timeLimit;
    private float timer;
    private InputController inputs;
    private Nexus nexus;
    [SerializeField] private GameObject winMenu;
    void Start()
    {
        inputs = FindObjectOfType<InputController>();
        nexus = FindObjectOfType<Nexus>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameModes){
            case GameModes.Scenario:
                break;
            case GameModes.Crossfire:
                timer += Time.deltaTime;
                if(timer > timeLimit){
                    EndGame();
                }
                break;
            default:
                break; 
        }
    }

    private void EndGame(){
        inputs.RemoveSubscribers();
        nexus.DisableNexus();
        winMenu.SetActive(true);
    }
}
