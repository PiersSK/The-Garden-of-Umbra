using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FleeState : BaseState
{
    private float fleeDistance = 10f;
    private float initialSpeed;
    private Vector3 originalPosition;
    public override void Enter()
    {
        initialSpeed = creature.Agent.speed;
        originalPosition = creature.transform.position;
    }

    public override void Perform()
    {
        if (creature.CanDetectPlayer() && creature.isSkittish)
        {
            Flee();
            if (Vector3.Distance(creature.transform.position, originalPosition) > fleeDistance)
            {
                stateMachine.ChangeState(new DespawnedState());
            }
        }
        else
        {
            creature.Agent.SetDestination(creature.path.waypoints[0].position);
            stateMachine.ChangeState(new IdleState());
        }
    }

    public override void Exit()
    {
        creature.Agent.speed = initialSpeed;
    }

    public void Flee()
    {
        Vector3 fleeDirection = (creature.Player.transform.position - creature.transform.position).normalized;
        creature.Agent.SetDestination(creature.transform.position + (-fleeDirection * fleeDistance));
        creature.Agent.speed = creature.PlayerController.speed;
    }
}
