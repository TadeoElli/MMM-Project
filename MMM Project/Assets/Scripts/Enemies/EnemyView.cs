using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyView : MonoBehaviour
{
    private EnemyBehaviour enemy;
    [SerializeField] private Image hpLeft,hpRight;
    private Image currentBar;

    public void SetHpImage(bool direction){
        if(direction){
            currentBar = hpLeft;
        }else
        {
            currentBar = hpRight;
        }
    }
    public void TakeDamageView(float currentHp, float maxHp){
        currentBar.fillAmount = currentHp / maxHp;
    }
    private void OnMouseEnter() {
        currentBar.gameObject.SetActive(true);
    }
    private void OnMouseExit() {
        currentBar.gameObject.SetActive(false);
    }
}
