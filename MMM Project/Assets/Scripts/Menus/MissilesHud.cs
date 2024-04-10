using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class MissilesHud : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// Esta clase se va a encargar de modificar los iconos de los missiles en el hud y sus descripciones
    /// </summary>
    [SerializeField] private Image image, hoverImage, pressedImage, anim;
    [SerializeField] private GameObject description;
    private bool isInteractable,isEnter;
    private float currentAmount, maxAmount;

    private void Start() {
        maxAmount = 2;
    }

    private void Update(){
        if(currentAmount < maxAmount){
            currentAmount = currentAmount + 1 * Time.deltaTime;
            if(image!=null){image.fillAmount = Mathf.Clamp(currentAmount,0,2) / maxAmount;}
        }
        else{isInteractable = true;}

        if(isEnter){   
            if(description != null){description.SetActive(true); }
            if(Input.GetMouseButtonDown(0)){
                if(pressedImage!=null)pressedImage.gameObject.SetActive(true);
            }
            else if(Input.GetMouseButtonUp(0)){
                if(pressedImage!=null)pressedImage.gameObject.SetActive(false);
            }
        }
        else
        {
            if(description!=null){description.SetActive(false);}
        }
    }

    public void OnPointerEnter(PointerEventData eventData){
        if(isInteractable){
            if(hoverImage!=null)hoverImage.gameObject.SetActive(true);
            if(anim!=null){anim.gameObject.SetActive(true);}
            isEnter = true;
            
        }
    }
    public void OnPointerExit(PointerEventData eventData){
        if(hoverImage!=null)hoverImage.gameObject.SetActive(false);
        if(pressedImage!=null)pressedImage.gameObject.SetActive(false);
        if(anim!=null){anim.gameObject.SetActive(false);}
        isEnter = false;
    }

    public void SetCurrentAmount(float amount){
        currentAmount = amount;
        isInteractable = false;
    }
}