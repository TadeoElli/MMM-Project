using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VolumeBar: MonoBehaviour
{
    /// <summary>
    /// Esta clase sirve para controlar el valor de volumen de los controladores de audio
    /// </summary>

    public void SetMusicVolume(float amount){
        AudioManager.Instance.SetMusicVolume(amount);
    }
    public void SetEffectVolume(float amount){
        AudioManager.Instance.SetEffectsVolume(amount);
    }
    

}