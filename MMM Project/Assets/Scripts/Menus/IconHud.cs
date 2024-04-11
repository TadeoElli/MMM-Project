using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public abstract class IconHud : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// Esta clase sirve como base para manejar los iconos del hud
    /// </summary>
    [SerializeField] private Sprite image, cooldownImage;
    protected Image imageComp;
    protected bool isInteractable,isEnter;
    protected float currentAmount, maxAmount;


    private void Update(){
        if(currentAmount < maxAmount){
            currentAmount = currentAmount + 1 * Time.deltaTime;
            if(imageComp!= null){imageComp.sprite = cooldownImage;}
            if(imageComp!=null){imageComp.fillAmount = Mathf.Clamp(currentAmount,0,maxAmount) / maxAmount;}
        }
        else{
            if(imageComp!= null){imageComp.sprite = image;}
            if(imageComp!=null){imageComp.fillAmount = 1;}
            isInteractable = true;
        }

        if(isEnter){   
            if(Input.GetMouseButtonDown(0)){
                OnClickDown();
            }
            else if(Input.GetMouseButtonUp(0)){
                OnClickUp();
            }
        }
    }
    protected abstract void OnClickDown();
    protected abstract void OnClickUp();
    protected abstract void OnClickEnter();
    protected abstract void OnClickExit();
    public void OnPointerEnter(PointerEventData eventData){
        if(isInteractable){
            isEnter = true;
            OnClickEnter(); 
        }
    }
    public void OnPointerExit(PointerEventData eventData){
        isEnter = false;
        OnClickExit();
    }

    public void SetCurrentAmount(){
        currentAmount = 0;
        isInteractable = false;
    }
}