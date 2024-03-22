using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PowerUp : MonoBehaviour
{
   [SerializeField] private float timer, cooldown;
   [SerializeField] Image cooldownImage, cooldownFeedback;
   public UnityEvent callAction;
    private void OnEnable() {
        timer = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < 0){
            this.gameObject.SetActive(false);
        }
        else{
            timer = timer - 1 * Time.deltaTime;
        }
        cooldownImage.fillAmount = timer / cooldown;
    }

    private void OnMouseOver() {
        cooldownFeedback.gameObject.SetActive(true);
        if(Input.GetMouseButtonDown(0)){
            callAction?.Invoke();
        }
    }
    private void OnMouseExit() {
        cooldownFeedback.gameObject.SetActive(false);
    }

    public void EnergyPowerUp(int cooldown){
        NexusStats nexus = FindObjectOfType<NexusStats>();
        nexus.EnergyPowerUp(cooldown);
    }

    public void StructurePowerUp(int cooldown){
        NexusStats nexus = FindObjectOfType<NexusStats>();
        nexus.StructurePowerUp(cooldown);
    }

    public void StabilityPowerUp(int cooldown){
        NexusStats nexus = FindObjectOfType<NexusStats>();
        nexus.StabilityPowerUp(cooldown);
    }
    public void DesactivatePowerUp(){
        this.gameObject.SetActive(false);
    }

}