using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Observer<int> currentIndex = new Observer<int>(0);
    [SerializeField] private TowerStrategy [] towers;
    
    [SerializeField] private List<float> cooldowns;
    [SerializeField] private List<bool> isReady;
    void Start()
    {
        for (int i = 0; i < towers.Length; i++)
        {
            if(i == 0){
                cooldowns.Add(0);
                isReady.Add(false);
            }
            else{
                cooldowns.Add(towers[i].cooldown);
                isReady.Add(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTowerIndex(int index){
        if(!isReady[index]){
            currentIndex.Value = 4;
        }
        else{
            currentIndex.Value = index;
        }
    }
}
