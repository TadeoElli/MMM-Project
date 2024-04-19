using UnityEngine;

public abstract class TowerStrategy : ScriptableObject        //Strategy para todos los tipos de torretas
{
    public float energyConsumption; //La cantidad de energia que consume con el tiempo
    public float maxEnergy; //La cantidad de energia maxima que va a tener y se ira desgastando con el tiempo
    public float cooldown;  //El cooldown de espera para volver a colocar esta torreta
    public TowerBehaviour prefab;   //El prefab que se va a crear cuando se clickee
    public Explosion explosion; //La explosion que se va a generar cuando la torreta pierda toda su energia
    [Header("Cursor Properties")]
    public Sprite sprite;   //La imagen que debe mostrar el cursor
    public Material material;   //El material del cursor
    public Vector3 scale;   //La escala del cursor
    [Header("Sound Effects")]
    public AudioClip invalidEffect, deployEffect;
    public void CreateTower(Vector2 origin){        //Esta funcion crea la torreta en el punto donde se presiono
        GameObject tower = TowersPool.Instance.RequestTower(prefab);
        tower.transform.position = origin;
        AudioManager.Instance.PlaySoundEffect(deployEffect);
    }
    public abstract void SpecialBehaviour(GameObject prefab, GameObject other); //El comportamiento especial que tiene la torre

    public abstract bool ColliderBehaviour(GameObject prefab, GameObject other);    //El comportamiento por colision que tiene la torre

    public abstract void DestroyTower(GameObject prefab);   //El comportamiento que tiene al destruirse
    

    
}