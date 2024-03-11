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
    [SerializeField] private List<GameObject> gravityScale;
    private int  powerIndex,towerIndex;
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
        powerIndex = index;
        DesactivateAllCursors();
        if(index > 0){
            powers[powerIndex].SetActive(true);
        }
    }
    public void ChangePowerState(bool state){
        DesactivateAllCursors();
        if(state){
            RaycastHit2D hit = Physics2D.Raycast(currentPosition, Vector2.zero);
            if(hit.collider != null && hit.collider.CompareTag("Enemy")){
                if(hit.collider.gameObject.layer == 8 ){
                    gravityScale[0].SetActive(true);
                }
                else if(hit.collider.gameObject.layer == 9){
                    gravityScale[1].SetActive(true);
                }
                else if(hit.collider.gameObject.layer == 10 ){
                    gravityScale[2].SetActive(true);
                }
            }
            else{
                powers[powerIndex + 2].SetActive(true);
            }
        }
    }
    private void DesactivateAllCursors(){
        for (int i = 1; i < powers.Count; i++)
        {
            powers[i].SetActive(false); 
        }
        for (int i = 0; i < gravityScale.Count; i++)
        {
            gravityScale[i].SetActive(false);
        }
    }
    public void SetTowerIndex(int index){
        towerIndex = index;
    }
}
