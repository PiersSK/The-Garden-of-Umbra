using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnedState : BaseState
{
    // Start is called before the first frame update
    public override void Enter()
    {
        creature.MeshRenderer.renderingLayerMask = 0;
        creature.Agent.SetDestination(creature.path.waypoints[0].position);
        creature.Agent.transform.position = creature.path.waypoints[0].position;
    }

    public override void Perform()
    {
        if(!creature.CanDetectPlayer())
        {
            stateMachine.ChangeState(new IdleState());
        }
    }

    // Update is called once per frame
    public override void Exit()
    {
        creature.MeshRenderer.renderingLayerMask = 2;
    }
}
