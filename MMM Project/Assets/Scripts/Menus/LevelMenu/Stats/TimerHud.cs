using TMPro;
using UnityEngine;

public class TimerHud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;
    bool isOff = true;
    [SerializeField] private GameObject winMenu;


    // Update is called once per frame
    void Update()
    {
        if(isOff)
            return;
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if (elapsedTime > 180)
            WinGame();
    }
    public void ResetTimer()
    {
        elapsedTime = 0;
        isOff = false;
    }    
    private void WinGame()
    {
        GameManager.Instance.EndGame();
        winMenu.SetActive(true);
    }
}
