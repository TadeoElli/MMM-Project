using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    Vector2 currentPosition;
    [SerializeField] private SpriteRenderer missileCursorExt, missileCursorInt;
    [Header("List Of Colors")]
    [SerializeField] private List<Color> colors;
    [Header("List Of PowerCursors")]
    [SerializeField] private List<GameObject> powers;
    private int  towerIndex;
    void Start()
    {
        missileCursorExt.color = colors[0];
        missileCursorInt.color = colors[0];
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = currentPosition;
    }

    public void ChangeMissileCursorColor(int index){
        missileCursorExt.color = colors[index];
        missileCursorInt.color = colors[index];
    }
    public void ChangePowerCursor(int index){
        DesactivateAllCursors();
        if(index > 0){
            powers[index].SetActive(true);
        }
    }
    private void DesactivateAllCursors(){
        for (int i = 1; i < powers.Count; i++)
        {
            powers[i].SetActive(false);
            
        }
    }
    public void SetTowerIndex(int index){
        towerIndex = index;
    }
}
