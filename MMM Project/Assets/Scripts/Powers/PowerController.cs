using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Observer<int> currentIndex = new Observer<int>(0);
    Camera cam;
    [SerializeField] private bool hasPower = false;
    private bool isDraggin = false;

    [SerializeField] private PowerStrategy [] powers;
    
    [SerializeField] private List<float> cooldowns;
    [SerializeField] private List<float> currentCd;
    [SerializeField] private List<bool> isReady;
    private void Awake() {
        cam = Camera.main;
    }
    void Start()
    {
        for (int i = 0; i < powers.Length; i++)
        {
            if(i == 0){
                cooldowns.Add(0);
                currentCd.Add(0);
                isReady.Add(false);
            }
            else{
                cooldowns.Add(powers[i].cooldown);
                currentCd.Add(powers[i].cooldown);
                isReady.Add(true);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i < powers.Length; i++)
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

    private void FixedUpdate() {
        if(hasPower && isDraggin){
            if(!powers[currentIndex.Value].BehaviourPerformed()){

                Debug.Log("performed");
            }
            
        }
    }

    public void SetPowerIndex(int newIndex){
        if(!isReady[newIndex]){
            currentIndex.Value = 0;
            hasPower = false;
        }
        else{
            currentIndex.Value = newIndex;
            hasPower = true;
        }
    }
    public void ActivatePower(InputAction.CallbackContext callbackContext){
        if(hasPower){
            if(callbackContext.started){
                if(powers[currentIndex.Value].BehaviourStarted()){
                    isDraggin = true;
                }
                else{
                    DesactivatePower();
                }
            }
            else if(callbackContext.canceled){
                powers[currentIndex.Value].BehaviourEnded();
                isReady[currentIndex.Value] = false;
                currentCd[currentIndex.Value] = 0;
                currentIndex.Value = 0;

            }
        }
    }

    public void DesactivatePower(){
        if(hasPower){
            hasPower = false;
            currentIndex.Value = 0;
        }
    }


}

