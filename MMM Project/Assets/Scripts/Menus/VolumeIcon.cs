using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VolumeIcon: MonoBehaviour
{
    /// <summary>
    /// Esta clase sirve como base para activar o desactivar el volumen
    /// </summary>

    public void ActivateVolume(){
        AudioManager.Instance.SetMusicVolume(0.3f);
    }
    public void DesactivateVolume(){
        AudioManager.Instance.SetMusicVolume(0f);
    }
    

}