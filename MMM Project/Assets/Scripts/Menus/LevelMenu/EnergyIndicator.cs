using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyIndicator : MonoBehaviour
{
    /// <summary>
    /// Esta clase se va a encargar de modificar la posicion del indicador de la barra de energia para mostrar cuanta energia se consumira
    /// </summary>
    [SerializeField] private Image indicator;
    [SerializeField]private float minX, maxX;
    private float currentPosition, maxEnergy;
    [SerializeField] private Rect rect;



    public void SetPosition(float energyCost){
        currentPosition = ((energyCost * maxX) / maxEnergy) - minX;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(currentPosition,0);
    }

    public void SetMaxAmount(float amount){
        maxEnergy = amount;
    }
}
