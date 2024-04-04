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
            hpRight.gameObject.SetActive(false);
            currentBar = hpLeft;
        }else
        {
            hpLeft.gameObject.SetActive(false);
            currentBar = hpRight;
        }
    }
    public void TakeDamageView(float currentHp, float maxHp){
        currentBar.fillAmount = currentHp / maxHp;
    }
}
