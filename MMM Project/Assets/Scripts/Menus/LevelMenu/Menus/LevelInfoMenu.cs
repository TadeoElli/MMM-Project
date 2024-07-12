using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LevelInfoMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<GameObject> content;
    [SerializeField] private int index = 0;
    [SerializeField] private Button previusButton, nextButton;

    void StartState()
    {
        content.ForEach(x => x.SetActive(false));
        previusButton.interactable = false;
        nextButton.interactable = true;
    }

    private void ResetContent()
    {
        content.ForEach(x => x.SetActive(false));
        previusButton.interactable = true;
        nextButton.interactable = true;
        content[index].SetActive(true);  
    }
    public void NextPage(){
        index++;
        ResetContent();
        if(index == content.Count - 1){
            nextButton.interactable = false;
        }
    }
    public void PreviusPage(){
        index--;
        ResetContent();
        if(index == 0){
            previusButton.interactable = false;
        }
    }
    public void StartPage(){
        index = 0;
        ResetContent();
        previusButton.interactable = false;
    }


}
