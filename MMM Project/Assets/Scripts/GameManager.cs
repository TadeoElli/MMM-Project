using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    [SerializeField] private Scenes gameMode;
    [SerializeField] private float timeLimit;
    private float timer;
    private int _enemiesAlive;
    public int EnemiesAlive { get{return _enemiesAlive;}set { _enemiesAlive = value;}}
    [SerializeField] private GameObject winMenu;
    private void Awake() {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameMode){
            case Scenes.MainMenu:
                break;
            case Scenes.Crossfire:
                timer += Time.deltaTime;
                if(timer > timeLimit && _enemiesAlive == 0){
                    EndGame();
                }
                break;
            default:
                break; 
        }
    }

    private void EndGame(){
        InputController.Instance.RemoveSubscribers();
        Nexus.Instance.DisableNexus();
        //winMenu.SetActive(true);
    }
    public void SetEnemiesAlive(int amount){
        _enemiesAlive = amount;
    }
    public void SetGameMode(Scenes scene){
        gameMode = scene;
    }
}
