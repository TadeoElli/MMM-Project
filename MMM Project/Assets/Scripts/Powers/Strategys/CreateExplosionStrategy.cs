using UnityEngine;



[CreateAssetMenu(fileName = "New Power", menuName = "ScriptableObject/Power/Explosion", order = 2)]
public class CreateExplosionStrategy : PowerStrategy
{
    /// <summary>
    /// Este tipo de poderes solo crean una explosion, por lo que el comportamiento se activa al soltar el click
    /// </summary>
    [Header("Special Properties")]
    [SerializeField]private Explosion prefab;   //La explosion a crear
    public override bool BehaviourStarted(){    //El comportamiento al presionar, si devuelve true pasa al siguiente estado
        return true;
    }



    public override bool BehaviourPerformed(){  //El comportamiento al mantener presionado, si devuelve true es porque no tiene comportamiento mientras se presiona
        return true;
    }
    public override void BehaviourEnded(){  //El comportamiento al soltar
        Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CreateExplosion(origin);
    }

    private void CreateExplosion(Vector2 origin){   //Crea la explosion correspondiente
        GameObject explosion = ExplosionPool.Instance.RequestExplosion(prefab);
        explosion.transform.position = origin;
    }
}
