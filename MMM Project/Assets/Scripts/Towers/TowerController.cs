using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Observer<int> currentIndex = new Observer<int>(0);
    Camera cam;
    [SerializeField] private bool hasTower = false;

    [SerializeField] private TowerStrategy [] towers;
    
    [SerializeField] private List<float> cooldowns;
    [SerializeField] private List<float> currentCd;
    [SerializeField] private List<bool> isReady;

    private void Awake() {
        cam = Camera.main;
    }
    void Start()
    {
        for (int i = 0; i < towers.Length; i++)
        {
            if(i == 0){
                cooldowns.Add(0);
                currentCd.Add(0);
                isReady.Add(false);
            }
            else{
                cooldowns.Add(towers[i].cooldown);
                currentCd.Add(towers[i].cooldown);
                isReady.Add(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i < towers.Length; i++)
        {
            if(!isReady[i]){
                if(currentCd[i] >= cooldowns[i]){
                    isReady[i] = true;
                }
                else
                {
                    currentCd[i] = currentCd[i] + 1 * Time.deltaTime;
                    currentCd[i] = Mathf.Clamp(currentCd[i], 0, cooldowns[i]);
                }
            }
        }
    }

    public void SetTowerIndex(int newIndex){
        if(!isReady[newIndex]){
            currentIndex.Value = 0;
            hasTower = false;
        }
        else{
            currentIndex.Value = newIndex;
            hasTower = true;
        }
    }
    public void ActivateTower(){
        if(hasTower){
            CreateTower();
            currentIndex.Value = 0;
        }
    }
    public void DesactivateTower(){
        if(hasTower){
            hasTower = false;
            currentIndex.Value = 0;
        }
    }

    private void CreateTower(){
        towers[currentIndex.Value].CreateTower(cam.ScreenToWorldPoint(Input.mousePosition));
        hasTower = false;
        isReady[currentIndex.Value] = false;
        currentCd[currentIndex.Value] = 0;
    }
}
