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
    /// 
    [SerializeField] protected bool isInteractable,isEnter;


    protected virtual void Update(){
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


}