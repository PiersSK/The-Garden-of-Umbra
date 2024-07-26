using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class CottonSpriteFollowState : BaseState
{

    private Creatures creature;
    private GameObject player;
    private NavMeshAgent creatureAgent;
    public CottonSpriteFollowState(StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, GameObject player) : base(stateMachine) 
    {
        this.creature = creature;
        this.player = player;
        this.creatureAgent = creatureAgent;
    }
    public override void Enter()
    {
    }

    public override void Perform()
    {
        FollowPlayer();
    }
    public override void Exit()
    {

    }

    public void FollowPlayer()
    {
        creatureAgent.SetDestination(player.transform.position);
        if (Vector3.Distance(player.transform.position, creatureAgent.transform.position) < creatureAgent.stoppingDistance)
        {
            stateMachine.ChangeState("CottonSpriteSittingState");
        }
        if (Vector3.Distance(player.transform.position, creatureAgent.transform.position) > 5f)
        {
            stateMachine.ChangeState("CottonSpriteWanderState");
        }
        
    }
}
