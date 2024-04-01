using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InputController : MonoBehaviour 
{
    /// <summary>
    /// esta clase se encarga de manegar los inputs y los indices
    /// </summary>
    public Observer<int> missileIndex = new Observer<int>(0);   //El indice de misiles que notificara al resto
    public Observer<int> towerIndex = new Observer<int>(0);  //El indice de torres que notificara al resto
    //[SerializeField] private int missileIndex;
    public Observer<int> powerIndex = new Observer<int>(0);  //El indice de poderes que notificara al resto

    public bool isAvailable = true; //si esta disponible para modificar cualquier indice o no
    public bool missileIsAvailable = true;  //Si esta disponible para modificar el indice de los misiles

    public void SetMissileIndex(int index){ //Setea el indice de los misiles si esta permitido
        if(missileIsAvailable){
            missileIndex.Value = index;
        }
        
    }
    public void SetTowerIndex(int index){   //Setea el indice de los torres si esta permitido
        if(isAvailable){
            towerIndex.Value = index;
        }
    }
    public void SetPowerIndex(int index){   //Setea el indice de los poderes si esta permitido
        if(isAvailable){
            powerIndex.Value = index;
        }
    }

    public void RestoreIndex(int cooldown){ 
        Invoke("Restore", cooldown);
    }
    private void Restore(){ //Restablece el indice de los misiles
        missileIsAvailable = true;
        missileIndex.Value = 0;
    }
}
