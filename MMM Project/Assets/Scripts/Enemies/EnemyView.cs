using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyView : MonoBehaviour
{
    [SerializeField] private Image hpLeft,hpRight, hpBarLeft, hpBarRight;
    private Image currentBar, currentHpBar;

    public void SetHpImage(bool direction){
        currentBar = direction ? hpLeft : hpRight;
        currentHpBar = direction ? hpBarLeft : hpBarRight;
        currentBar.fillAmount = 1;
    }
    public void TakeDamageView(float currentHp, float maxHp){
        currentBar.fillAmount = currentHp / maxHp;
    }
    private void OnMouseEnter()
    {
        SetHpBarVisibility(true);
    }

    private void OnMouseExit()
    {
        SetHpBarVisibility(false);
    }
    private void SetHpBarVisibility(bool visible)
    {
        currentBar.gameObject.SetActive(visible);
        currentHpBar.gameObject.SetActive(visible);
    }
}
