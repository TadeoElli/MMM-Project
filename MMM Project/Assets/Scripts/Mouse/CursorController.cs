using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    Vector2 currentPosition;
    [SerializeField] private SpriteRenderer missileCursorExt, missileCursorInt;
    [Header("List Of Colors")]
    [SerializeField] private List<Color> colors;
    private int missileIndex, powerIndex, towerIndex;
    void Start()
    {
        missileCursorExt.color = colors[0];
        missileCursorInt.color = colors[0];
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

    public void ChangeMissileCursorColor(int index){
        missileCursorExt.color = colors[index];
        missileCursorInt.color = colors[index];
    }
    public void SetMissileIndex(int index){
        missileIndex = index;
    }
    public void SetPowerIndex(int index){
        powerIndex = index;
    }
    public void SetTowerIndex(int index){
        towerIndex = index;
    }
}
