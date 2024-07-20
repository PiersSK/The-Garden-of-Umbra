using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class FriendlyState : BaseState
{
    public override void Enter()
    {
        creature.Agent.stoppingDistance = 2;
    }

    public override void Perform()
    {
        if(creature.CanDetectPlayer())
        {
            FollowPlayer();
        }
        else
        {
            creature.Agent.SetDestination(creature.path.waypoints[0].position);
            stateMachine.ChangeState(new IdleState());
        }
    }
    public override void Exit()
    {
        creature.Agent.stoppingDistance = 0;
    }

    public void FollowPlayer()
    {
        creature.Agent.SetDestination(creature.Player.transform.position);
    }
}
