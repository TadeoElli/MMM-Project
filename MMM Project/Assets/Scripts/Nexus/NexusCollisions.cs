using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NexusCollisions : MonoBehaviour, IGridEntity
///Esta clase guarda la vida del nexo y maneja sus collisiones, si una explosion surge dentro del area, le manda a esta clase el tipo de explosion
///y esta calcula y reduce la vida segun el daño correspondiente, si se queda sin vida, se destruye el nexo
{//IA2-P2”.
    public Observer<float> currentStructure = new Observer<float>(3000);
    [SerializeField] private float damage;
    [SerializeField] private GameObject loseMenu;   //El menu de derrota
    public event Action<IGridEntity> OnMove;
    private void Start() {
        IGridEntity gridEntity = GetComponent<IGridEntity>();
        if (gridEntity != null) {
            SpatialGrid.Instance.Add(gridEntity);
        }
    }

    public void TakeDamageForMissile(ExplosionsTypes type){//Recibe el tipo de daño, pregunta cuanto es y lo resta de la vida del nexo
        damage = DamageTypes.Instance.explosionDictionary[type];
        Debug.Log("Recibi "+ damage+ " de dano" );
        currentStructure.Value -= damage;
        if(currentStructure.Value <= 0){
            DestroyNexus();
        }
    }

    public void SetStructureValue(float amount){    //Recibe el valor de vida actual del NexusStats y lo guarda en la variable de vida local
        currentStructure.Value = amount;
        OnMove?.Invoke(this);
    }
    public Vector3 Position {
        get => transform.position;
        set => transform.position = value;
    }

    private void DestroyNexus() {
        loseMenu.SetActive(true);
        IGridEntity gridEntity = GetComponent<IGridEntity>();
        if (gridEntity != null) {
            SpatialGrid.Instance.Remove(gridEntity);
        }
        this.gameObject.SetActive(false);
    }

    private void OnDisable() {
        currentStructure.RemoveAllListeners();
    }
        
}
