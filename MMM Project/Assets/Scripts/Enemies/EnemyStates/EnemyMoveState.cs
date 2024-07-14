using FSM;
using System.Collections.Generic;
using UnityEngine;
//IA2-P3”.
public class EnemyMoveState : MonoBaseState
{
    [SerializeField] EnemyBehaviour owner;



    public override IState ProcessInput()
    {
        if (!owner.canMove && Transitions.ContainsKey(EnemyStateTransitions.ToDesactivate))
            return Transitions[EnemyStateTransitions.ToDesactivate];
        return this;
    }
    public override void Enter(IState from, Dictionary<string, object> transitionParameters = null)
    {
        base.Enter(from, transitionParameters);
        //Debug.Log("Entre al MoveForward");
    }

    public override void UpdateLoop()
    {
        owner.Rotate();
        owner.MoveForward();
        owner.UpdateParticle();
    }
}
