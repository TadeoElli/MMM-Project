using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class PowerHud : IconsTimerHud
{
    /// <summary>
    /// Esta clase se va a encargar de modificar los iconos de los poderes en el hud y sus descripciones
    /// </summary>
    [SerializeField] private Image hoverImage, pressedImage;
    [SerializeField] private EnergyIndicator indicator;
    [SerializeField] private PowerStrategy power;
    [SerializeField] private UnityEvent changeIndex;
    [SerializeField] private AudioClip readyEffect;
    private void Start() {
        imageComp = GetComponent<Image>();
        maxAmount = power.cooldown;
        currentAmount = power.cooldown;
    }

    protected override void OnClickEnter(){
        if(hoverImage!=null)hoverImage.gameObject.SetActive(true);
        if(indicator!=null){indicator.gameObject.SetActive(true);}
        indicator.SetPosition(power.energyConsumption);
    }
    protected override void OnClickExit(){
        if(hoverImage!=null)hoverImage.gameObject.SetActive(false);
        if(pressedImage!=null)pressedImage.gameObject.SetActive(false);
        if(indicator!=null){indicator.gameObject.SetActive(false);}
    }
    protected override void OnClickDown(){
        if(pressedImage!=null)pressedImage.gameObject.SetActive(true);
    }
    protected override void OnClickUp(){
        if(pressedImage!=null)pressedImage.gameObject.SetActive(false);
        changeIndex?.Invoke();
    }
    protected override void OnIconReady(){
        AudioManager.Instance.PlaySoundEffect(readyEffect);
    }

    public void SetCooldowns(float baseCooldown){
        maxAmount = power.cooldown - baseCooldown;
    }
}