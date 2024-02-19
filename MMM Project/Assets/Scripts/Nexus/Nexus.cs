using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : MonoBehaviour
{
    [SerializeField] private MissileStrategy [] missiles;
    [SerializeField] private GameObject mouseOverMissile;
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private int index = 0;
    [SerializeField] private bool haveMissile;
    void Start()
    {
        mouseOverMissile.SetActive(false);
        index = 0;
        missilePrefab = missiles[index].CreateMissile(transform);
        haveMissile = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!haveMissile){
            missilePrefab = missiles[index].CreateMissile(transform);
            haveMissile = true;
        }

        
    }

    private void OnMouseOver() {
        mouseOverMissile.SetActive(true);
    }

    private void OnMouseExit() {
        mouseOverMissile.SetActive(false);
    }
}
