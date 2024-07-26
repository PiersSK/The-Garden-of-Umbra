using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FleeState : BaseState
{
    private float fleeDistance = 5f;
    private float initialSpeed;

    public FleeState(StateMachine stateMachine) : base(stateMachine) {}
    public override void Enter()
    {
       // initialSpeed = creature.Agent.speed;
    }

    public override void Perform()
    {

    }

    public override void Exit()
    {
        //creature.Agent.speed = initialSpeed;
    }

    public void Flee()
    {

    }
}
