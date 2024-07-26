using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public int waypointIndex;
    public float waitTimer;
    public PatrolState(StateMachine stateMachine, int waypointIndex) : base(stateMachine) {}       
    public override void Enter()
    {
       PatrolCycle();
    }

    public override void Perform()
    {
        PatrolCycle();

    }
    public override void Exit()
    {

    }

    public void PatrolCycle()
    {

    }
}
