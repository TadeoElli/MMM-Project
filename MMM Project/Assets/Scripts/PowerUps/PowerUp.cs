using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PowerUp : MonoBehaviour
{
    /// <summary>
    /// Esta clase se encarga de administrar los iconos de los powerUps y sus funciones
    /// </summary>
   [SerializeField] private float timer, cooldown;  //El tiempo que dura el powerUp antes de desvanecerse
   [SerializeField] Image cooldownImage, cooldownFeedback;  //La imagen del temporizador del powerup y de cuando esta el mouse encima
   public UnityEvent callAction;    //el evento que va a desencadenar cuando se presione
    private void OnEnable() {   //Se establece el tiempo que va a estar activo
        timer = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < 0){  //Se reduce el tiempo hasta que sea menor a 0 y ahi se desactiva, y se actualiza la imagen
            this.gameObject.SetActive(false);
        }
        else{
            timer = timer - 1 * Time.deltaTime;
        }
        cooldownImage.fillAmount = timer / cooldown;
    }

    private void OnMouseOver() {    //Se activa la imagen de cuando esta el mouse encima y si se presiona el mouse llama a las actions que tiene
        cooldownFeedback.gameObject.SetActive(true);
        if(Input.GetMouseButtonUp(0)){
            callAction?.Invoke();
        }
    }
    private void OnMouseExit() {    //Se desactiva la imagen de cuando esta el mouse encima
        cooldownFeedback.gameObject.SetActive(false);
    }

    #region Actions 
    public void EnergyPowerUp(int cooldown){    //Llama a la funcion de EnergyPowerUp del nexusStats
        //Debug.Log("Energy");
        NexusStats nexus = FindObjectOfType<NexusStats>();
        nexus.EnergyPowerUp(cooldown);
    }

    public void StructurePowerUp(int cooldown){ //Llama a la funcion de StructurePowerUp del nexusStats
        NexusStats nexus = FindObjectOfType<NexusStats>();
        nexus.StructurePowerUp(cooldown);
    }

    public void StabilityPowerUp(int cooldown){ //Llama a la funcion de StabilityPowerUp del nexusStats
        NexusStats nexus = FindObjectOfType<NexusStats>();
        nexus.StabilityPowerUp(cooldown);
    }
    public void SpeedPowerUp(int cooldown){ //Llama a la funcion de SpeedPowerUp del nexusStats
        NexusStats nexus = FindObjectOfType<NexusStats>();
        nexus.SpeedPowerUp(cooldown);
    }
    public void CooldownPowerUp(int cooldown){  //Llama a la funcion de CooldownPowerUp del nexusStats
        NexusStats nexus = FindObjectOfType<NexusStats>();
        nexus.CooldownPowerUp(cooldown);
    }
    public void NuclearPowerUp(){   //Deshabilita nuevos inputs del inputController y cambia el indice de poderes al del poder Nuclear
        InputController inputController = FindObjectOfType<InputController>();
        inputController.SetPowerIndex(6);
        inputController.isAvailable = false;
    }
    public void SingularityPowerUp(){   //Deshabilita nuevos inputs del inputController y cambia el indice de poderes al del poder Singularity
        InputController inputController = FindObjectOfType<InputController>();
        inputController.SetPowerIndex(7);
        inputController.isAvailable = false;
    }
    public void AntimatterPowerUp(int cooldown){    //Deshabilita nuevos inputs del inputController para cambiar misiles y establece los misiles antimateria
        InputController inputController = FindObjectOfType<InputController>();
        inputController.SetMissileIndex(8);
        inputController.missileIsAvailable = false;
        inputController.RestoreIndex(cooldown);
    }
    public void DesactivatePowerUp(){   //Se desactiva este objeto
        Debug.Log("Desactivate");
        this.gameObject.SetActive(false);
    }
    #endregion
}
