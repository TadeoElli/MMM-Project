using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesFeedback : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI textComp;
    private int amount = 1;
    [SerializeField] private Animator anim;

    public void OnLivesChange(){
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("LivesFeedback")){
            amount = 1;
            textComp.text = "-" + amount;
            anim.SetTrigger("ShowFeedback");
        }
        else{
            amount++;
            textComp.text = "-" + amount;
        }
    }
}
