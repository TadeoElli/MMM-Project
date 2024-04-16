using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PowerUpHud : MonoBehaviour
{
    /// <summary>
    /// Esta clase sirve como base para manejar el hud de los powerUps
    /// </summary>
    [SerializeField] private Animator anim;
    [SerializeField] private TextMeshProUGUI text;
    private float timer;
    private bool isActive = false;
    private void Update() {
        if (timer > 0 && isActive){
            timer -= Time.deltaTime;
            text.text = timer.ToString();
        }
        else if( timer <= 0 && isActive){
            isActive = false;
            anim.SetBool("Show",false);
        }
    }
    public void ShowHud(int cooldown){
        timer = (float)cooldown;
        isActive = true;
        anim.SetBool("Show",true);
    }

}