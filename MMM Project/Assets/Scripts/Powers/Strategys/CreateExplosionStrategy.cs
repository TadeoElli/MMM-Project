using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Power", menuName = "ScriptableObject/Power/Explosion", order = 2)]
public class CreateExplosionStrategy : PowerStrategy
{
    private Rigidbody2D rb;
    [SerializeField]private Explosion prefab;
    public override bool BehaviourStarted(){
        return true;
    }



    public override bool BehaviourPerformed(){
        return true;
    }
    public override void BehaviourEnded(){
        Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CreateExplosion(origin);
    }

    private void CreateExplosion(Vector2 origin){
        GameObject explosion = ExplosionPool.Instance.RequestExplosion(prefab);
        explosion.transform.position = origin;
    }
}
