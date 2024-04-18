using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonsHud : IconHud
{
    /// <summary>
    /// Esta clase sirve como base para manejar los botones del hud que cuenten con un temporizador
    /// </summary>



    protected override void Update(){
        base.Update();

    }
    protected override void OnClickDown(){}
    protected override void OnClickUp(){}
    protected override void OnClickEnter(){}
    protected override void OnClickExit(){}

}