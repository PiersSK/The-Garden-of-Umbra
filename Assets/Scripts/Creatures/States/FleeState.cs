using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FleeState : BaseState
{
    private float fleeDistance = 10f;
    private float initialSpeed;
    public override void Enter()
    {
        stateName = "FleeState";
        initialSpeed = creature.Agent.speed;
    }

    public override void Perform()
    {

    }

    public override void Exit()
    {
        creature.Agent.speed = initialSpeed;
    }

    public void Flee()
    {

    }
}
