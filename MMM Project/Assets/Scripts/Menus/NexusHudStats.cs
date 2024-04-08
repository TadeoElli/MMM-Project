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
    [SerializeField] private IndicatorType type;
    private enum IndicatorType
    {
        barBetweenValues,
        WithPercentage,
        WithoutAny,
        Any
    }

    private void Update(){
        switch (type)
        {
            case IndicatorType.barBetweenValues:
                textComp.text = Mathf.FloorToInt(currentAmount).ToString() + "/" + Mathf.FloorToInt(maxAmount).ToString();
                break;
            case IndicatorType.WithPercentage:
                textComp.text = Mathf.FloorToInt(currentAmount).ToString() + "%";
                break;
            case IndicatorType.WithoutAny:
                textComp.text = currentAmount.ToString();
                break;
            case IndicatorType.Any:
                break;
            default:
                break;
        }
        if(image!=null){image.fillAmount = currentAmount / maxAmount;}
    }


    public void SetMaxAmount(float amount){
        maxAmount = amount;
    }
    public void SetCurrentAmount(float amount){
        currentAmount = amount;
    }
}
