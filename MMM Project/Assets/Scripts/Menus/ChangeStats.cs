using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using TMPro;

public class ChangeStats : MonoBehaviour
{
    /// <summary>
    /// Esta clase se va a encargar de modificar los textos del UI segun sea llamado por sus respectivos suscriptores
    /// </summary>
    [SerializeField] private LocalizedString localStringStat;
    [SerializeField] private TextMeshProUGUI textComp;
    private int amount;

    private void OnEnable() {
        localStringStat.Arguments = new object[] {amount};
        localStringStat.StringChanged += UpdateText;
    }

    private void OnDisable() {
        localStringStat.StringChanged -= UpdateText;
    }
    private void UpdateText(string value){
        textComp.text = value;
    }

    
    public void IncreaseAmount(int value){
        amount += value;
        localStringStat.Arguments[0] = amount;
        localStringStat.RefreshString();
    }
    public void DecreaseAmount(int value){
        amount -= value;
        Debug.Log(amount);
        localStringStat.Arguments[0] = amount;
        localStringStat.RefreshString();
    }
    public void SetAmount(int value){
        amount = value;
        localStringStat.Arguments[0] = amount;
        localStringStat.RefreshString();
    }
}
