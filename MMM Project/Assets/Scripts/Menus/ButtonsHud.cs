using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonsHud : IconHud
{
    /// <summary>
    /// Esta clase sirve como base para manejar los botones del hud que cuenten con un temporizador
    /// </summary>

    
    private Button button;

    private void Start(){
        button = GetComponent<Button>();
    }
    protected override void Update(){
        base.Update();
        isInteractable = button.interactable;
    }
    protected override void OnClickDown(){
        AudioManager.Instance.PlaySoundEffect(pressedEffect);
    }
    protected override void OnClickUp(){}
    protected override void OnClickEnter(){
        AudioManager.Instance.PlaySoundEffect(hooverEffect);
    }
    protected override void OnClickExit(){}

}