using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class FollowState : BaseState
{
    public override void Enter()
    {
        stateName = "FollowState";
        creature.Agent.stoppingDistance = 2;
    }

    public override void Perform()
    {

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
