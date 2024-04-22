using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissilesHud : IconsTimerHud
{
    /// <summary>
    /// Esta clase se va a encargar de modificar los iconos de los missiles en el hud y sus descripciones
    /// </summary>
    [SerializeField] private Image hoverImage, pressedImage, anim;
    [Header("Description")]
    [SerializeField] private GameObject description;
    [Header("Indicator")]
    [SerializeField] private EnergyIndicator indicator;
    [Header("Strategy")]
    [SerializeField] private MissileStrategy missile;
    private void Start() {
        imageComp = GetComponent<Image>();
        maxAmount = 2;
    }

    protected override void OnClickEnter(){
        if(hoverImage!=null)hoverImage.gameObject.SetActive(true);
        if(anim!=null){anim.gameObject.SetActive(true);} 
        if(description != null){description.SetActive(true); }
        if(indicator!=null){indicator.gameObject.SetActive(true);}
        indicator.SetPosition(missile.energyConsumption);
    }
    protected override void OnClickExit(){
        if(hoverImage!=null)hoverImage.gameObject.SetActive(false);
        if(pressedImage!=null)pressedImage.gameObject.SetActive(false);
        if(anim!=null){anim.gameObject.SetActive(false);}
        if(description!=null){description.SetActive(false);}
        if(indicator!=null){indicator.gameObject.SetActive(false);}
    }
    protected override void OnClickDown(){
        if(pressedImage!=null)pressedImage.gameObject.SetActive(true);
    }
    protected override void OnClickUp(){
        if(pressedImage!=null)pressedImage.gameObject.SetActive(false);
    }
    protected override void OnIconReady(){
        
    }
}