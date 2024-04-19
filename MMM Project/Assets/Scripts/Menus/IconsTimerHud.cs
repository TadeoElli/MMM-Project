using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class IconsTimerHud : IconHud
{
    /// <summary>
    /// Esta clase sirve como base para manejar los iconos del hud que cuenten con un temporizador
    /// </summary>
    [Header("Sprites")]
    [SerializeField] private Sprite image, cooldownImage;
    
    protected Image imageComp;
    protected float currentAmount, maxAmount;


    protected override void Update(){
        base.Update();
        if(currentAmount < maxAmount){
            currentAmount = currentAmount + 1 * Time.deltaTime;
            if(imageComp!= null){imageComp.sprite = cooldownImage;}
            if(imageComp!=null){imageComp.fillAmount = Mathf.Clamp(currentAmount,0,maxAmount) / maxAmount;}
            if(currentAmount >= maxAmount) OnIconReady();
        }
        else{
            if(imageComp!= null){imageComp.sprite = image;}
            if(imageComp!=null){imageComp.fillAmount = 1;}
            isInteractable = true;
        }


    }
    protected override void OnClickDown(){}
    protected override void OnClickUp(){}
    protected override void OnClickEnter(){}
    protected override void OnClickExit(){}
    protected abstract void OnIconReady();

    public void SetCurrentAmount(){
        currentAmount = 0;
        isInteractable = false;
    }
}