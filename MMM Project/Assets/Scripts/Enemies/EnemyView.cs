using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyView : MonoBehaviour
{
    [SerializeField] private Image hpLeft,hpRight, hpBarLeft, hpBarRight;
    private Image currentBar, currentHpBar;

    public void SetHpImage(bool direction){
        if(direction){
            currentBar = hpLeft;
            currentHpBar = hpBarLeft;
        }else
        {
            currentBar = hpRight;
            currentHpBar = hpBarRight;
        }
        currentBar.fillAmount = 1;
    }
    public void TakeDamageView(float currentHp, float maxHp){
        currentBar.fillAmount = currentHp / maxHp;
    }
    private void OnMouseEnter() {
        currentBar.gameObject.SetActive(true);
        currentHpBar.gameObject.SetActive(true);
    }
    private void OnMouseExit() {
        currentBar.gameObject.SetActive(false);
        currentHpBar.gameObject.SetActive(false);
    }
}
