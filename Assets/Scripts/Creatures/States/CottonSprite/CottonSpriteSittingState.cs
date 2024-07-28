using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class CottonSpriteSittingState : BaseState
{
    public float sittingTime = 5f;
    private float chanceToWander = 0.5f; // 0->1 as a percentage
    private float noticePlayerMaxRange = 8f;
    private float noticePlayerMinRange = 4f;
    public float timeSat = 0f;

    private bool shouldStand = false;
    private float standTimer = 0f;
    private float timeToStand = 2f;

    private string nextState = "CottonSpriteWanderState";

    private Creatures creature;
    private GameObject player;
    private NavMeshAgent creatureAgent;
    public CottonSpriteSittingState(StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, GameObject player) : base(stateMachine)
    {
        this.creature = creature;
        this.player = player;
        this.creatureAgent = creatureAgent;
    }
    public override void Enter()
    {
        creatureAgent.GetComponent<Animator>().SetBool("IsSitting", true);
    }

    public override void Perform()
    {
       Sitting();

        if (shouldStand)
        {
            standTimer += Time.deltaTime;
            if (standTimer >= timeToStand)
            {
                stateMachine.ChangeState(nextState);
            }
        }

    }

    public override void Exit()
    {
        creatureAgent.GetComponent<Animator>().SetBool("IsSitting", false);
        timeSat = 0f;
        shouldStand = false;
        standTimer = 0f;
    }
    
    public void Sitting()
    {
        float distanceToPlayer = Vector3.Distance(creatureAgent.transform.position, player.transform.position);
        Debug.Log(distanceToPlayer);

        timeSat += Time.deltaTime;
        if (timeSat >= sittingTime && distanceToPlayer > noticePlayerMaxRange)
        {
            if (Random.Range(0f, 1f) <= chanceToWander && shouldStand == false)
            {
                shouldStand = true;
                nextState = "CottonSpriteWanderState";
            }
            else timeSat = 0f;
        }
        else if (distanceToPlayer <= noticePlayerMaxRange && distanceToPlayer > noticePlayerMinRange)
        {
            shouldStand = true;
            nextState = "CottonSpriteFollowState";
        }
        else
        {
            shouldStand = false;
            standTimer = 0f;

            float xDistanceToPlayer = (player.transform.position - creatureAgent.transform.position).x;
            if (xDistanceToPlayer > 0)
                creatureAgent.GetComponent<SpriteRenderer>().flipX = false;
            else if (xDistanceToPlayer < 0)
                creatureAgent.GetComponent<SpriteRenderer>().flipX = true;
        }

        
    }
        
    
}
