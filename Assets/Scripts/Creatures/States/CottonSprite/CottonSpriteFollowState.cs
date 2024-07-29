using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class CottonSpriteFollowState : BaseState
{
    private bool shouldSit = false;
    private float sittingTimer = 0f;
    private float timeToSit = 0.5f;

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


        if (shouldSit)
        {
            sittingTimer += Time.deltaTime;
            if (sittingTimer >= timeToSit)
            {
                stateMachine.ChangeState("CottonSpriteSittingState");
            }
        }
    }
    public override void Exit()
    {
        creatureAgent.GetComponent<Animator>().SetBool("IsWalking", false);
        shouldSit = false;
        sittingTimer = 0f;
    }

    public void FollowPlayer()
    {
        Vector3 playerPos = player.transform.position;
        playerPos.y = creatureAgent.transform.position.y;
        creatureAgent.SetDestination(playerPos);

        // Is at player
        if (Vector3.Distance(player.transform.position, creatureAgent.transform.position) < creatureAgent.stoppingDistance)
        {
            creatureAgent.GetComponent<Animator>().SetBool("IsWalking", false);
            shouldSit = true;
        } else
        {
            creatureAgent.GetComponent<Animator>().SetBool("IsWalking", true);
            shouldSit = false;
            sittingTimer = 0f;
        }

        // Is too far from player
        if (Vector3.Distance(player.transform.position, creatureAgent.transform.position) > 8f)
        {
            stateMachine.ChangeState("CottonSpriteWanderState");
        }
        
    }
}
