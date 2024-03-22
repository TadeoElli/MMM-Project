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
    [SerializeField] private GameObject missileManipulatorPerformedCursor;
    [Header("List Of TowerCursors")]
    [SerializeField] private List<GameObject> towers;
    [SerializeField] private GameObject blockSprite;
    [SerializeField] private float distanceFromNexus;
    [SerializeField] private GameObject nexus;
    private int  powerIndex, towerIndex;
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

        if(towerIndex > 0 && CheckDistanceFromNexus()){
            blockSprite.SetActive(true);
        }
        else{
            blockSprite.SetActive(false);
        }
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

    public void ChangeTowerCursor(int index){
        towerIndex = index;
        DesactivateAllCursors();
        if(index > 0){
            towers[towerIndex].SetActive(true);
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
                missileManipulatorPerformedCursor.SetActive(true);
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
        for (int i = 1; i < towers.Count; i++)
        {
            towers[i].SetActive(false);
        }
        blockSprite.SetActive(false);
        missileManipulatorPerformedCursor.SetActive(false);
    }

    private bool CheckDistanceFromNexus(){
        if(Vector2.Distance(currentPosition, nexus.transform.position) < distanceFromNexus){
            return true;
        }
        else{
            return false;
        }
    }
    
}
