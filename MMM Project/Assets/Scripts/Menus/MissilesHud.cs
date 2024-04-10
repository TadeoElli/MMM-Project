using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissilesHud : MonoBehaviour
{
    /// <summary>
    /// Esta clase se va a encargar de modificar los numeros del UI de los misiles del hud sea llamado por sus respectivos suscriptores
    /// </summary>
    [SerializeField] private Image image;
    private float currentAmount, maxAmount;

    private void Start() {
        maxAmount = 2;
    }

    private void Update(){
        currentAmount = currentAmount + 1 * Time.deltaTime;
        if(image!=null){image.fillAmount = Mathf.Clamp(currentAmount,0,2) / maxAmount;}
    }


    public void SetCurrentAmount(float amount){
        currentAmount = amount;
    }
}