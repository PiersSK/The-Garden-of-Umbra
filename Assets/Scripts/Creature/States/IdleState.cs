using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public int waypointIndex;
    public float waitTimer;
    public override void Enter()
    {
       IdleCycle();
    }

    public override void Perform()
    {
        IdleCycle();
        if (creature.CanDetectPlayer())
        {
            if(!creature.isSkittish)
                stateMachine.ChangeState(new FriendlyState());
            else 
                stateMachine.ChangeState(new FleeState());
        }

    }
    public override void Exit()
    {

    }

    public void IdleCycle()
    {
        if (creature.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 3) 
            {
            if(waypointIndex < creature.path.waypoints.Count - 1)
                waypointIndex++;
            else
                waypointIndex = 0;
            creature.Agent.SetDestination(creature.path.waypoints[waypointIndex].position);
            waitTimer = 0;
            }
        }
    }
}
