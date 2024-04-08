using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class NexusHudStats : MonoBehaviour
{
    /// <summary>
    /// Esta clase se va a encargar de modificar los numeros del UI del hud sea llamado por sus respectivos suscriptores
    /// </summary>
    [SerializeField] private TextMeshProUGUI textComp;
    [SerializeField] private Image image;
    private float currentAmount, maxAmount;


    private void Update(){
        textComp.text = Mathf.FloorToInt(currentAmount).ToString() + "/" + Mathf.FloorToInt(maxAmount).ToString();
        image.fillAmount = currentAmount / maxAmount;
    }


    public void SetMaxAmount(float amount){
        maxAmount = amount;
    }
    public void SetCurrentAmount(float amount){
        currentAmount = amount;
    }
}
