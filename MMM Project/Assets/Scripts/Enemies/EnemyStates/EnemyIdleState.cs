using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
//IA2-P3”.
public class EnemyIdleState : MonoBaseState
{
    [SerializeField] EnemyBehaviour owner;
    [SerializeField] Rigidbody2D rb2D;
    float timer;

    public override IState ProcessInput()
    {
        if(!owner.canMove && Transitions.ContainsKey(EnemyStateTransitions.ToDesactivate))
            return Transitions[EnemyStateTransitions.ToDesactivate];
        if(rb2D.velocity.magnitude < 0.2){  //Si la velocidad del objeto es lo suficiente mente chica, procede a moverse, esto sirve para que 
        //cuando la velocidad aumente debido a una colision o un comportamiento, espere a que se detenga para pasar al estado que se encarga de rotar, desp de un tiempo
            timer = timer + 1 * Time.deltaTime;
            if(timer > 1.5f){
                if(owner.IsFacingDirection() && Transitions.ContainsKey(EnemyStateTransitions.ToMoveForward))
                    return Transitions[EnemyStateTransitions.ToMoveForward];       //Paso al estado de moverse
                else if(Transitions.ContainsKey(EnemyStateTransitions.ToRotate)){
                    return Transitions[EnemyStateTransitions.ToRotate];       //Paso al estado de rotar
                }
            }
        }
        
        return this;
        
    }

    public override void Enter(IState from, Dictionary<string, object> transitionParameters = null)
    {
        base.Enter(from, transitionParameters);
        timer = 0;
        Debug.Log("Entre al idle");
    }

    public override void UpdateLoop()
    {

    }
}
