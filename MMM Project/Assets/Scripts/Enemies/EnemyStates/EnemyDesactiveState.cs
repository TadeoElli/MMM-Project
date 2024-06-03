using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
//IA2-P3”.
public class EnemyDesactiveState : MonoBaseState
{
    [SerializeField] EnemyBehaviour owner;


    public override IState ProcessInput()
    {
        if(owner.canMove && Transitions.ContainsKey(EnemyStateTransitions.ToIdle))
            return Transitions[EnemyStateTransitions.ToIdle];
        return this;
    }

    public override void UpdateLoop()
    {
        owner.UpdateParticle();
    }
}
