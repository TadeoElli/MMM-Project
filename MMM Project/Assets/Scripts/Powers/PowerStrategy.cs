using UnityEngine;

public abstract class PowerStrategy : ScriptableObject        //Strategy para todos los tipos de poderes
{
    public float energyConsumption; //La energia que consumen al utilizarse, si no consumen nada se pone en 0
    public float cooldown;      //El cooldown para volver a lanzarse
    public bool hasPerformedCursor; //Si tiene un cursor aparte cuando se mantiene presionado
    [Header("Cursor Properties")]
    public Sprite sprite;   //El sprite que tiene que tener el cursor
    public Material material;   //el material que tiene que tener el cursor
    public Vector3 scale;   //la escala que tiene que tener el cursor

    public abstract bool BehaviourStarted();    //El comportamiento cuando se presiona el boton del mouse
    public abstract bool BehaviourPerformed();  //El comportamiento cuando se mantiene presionado el boton del mouse
    public abstract void BehaviourEnded();  //El comportamiento cuando se suelta el boton del mouse
    
}
