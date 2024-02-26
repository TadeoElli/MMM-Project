using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Observer<int> currentIndex = new Observer<int>(1);
    [SerializeField] private TowerStrategy [] towers;
    
    [SerializeField] private float[] cooldowns;
    [SerializeField] private bool[] isReady;
    void Start()
    {
        for (int i = 0; i < towers.Length; i++)
        {
            cooldowns[i] = towers[i].cooldown;
            isReady[i] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
