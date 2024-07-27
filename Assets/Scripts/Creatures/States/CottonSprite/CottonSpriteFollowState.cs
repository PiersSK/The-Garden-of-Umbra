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
        creatureAgent.stoppingDistance = 3f;
        creatureAgent.GetComponent<Animator>().SetBool("IsWalking", true);
    }

    public override void Perform()
    {
        FollowPlayer();
        if (creatureAgent.velocity.x > 0)
            creatureAgent.GetComponent<SpriteRenderer>().flipX = false;
        else if (creatureAgent.velocity.x < 0)
            creatureAgent.GetComponent<SpriteRenderer>().flipX = true;
    }
    public override void Exit()
    {
        creatureAgent.GetComponent<Animator>().SetBool("IsWalking", false);
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
