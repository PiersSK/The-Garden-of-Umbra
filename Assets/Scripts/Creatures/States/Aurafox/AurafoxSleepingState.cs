using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AurafoxSleepingState : BaseState
{
    private Creatures creature;
    private GameObject player;
    private NavMeshAgent creatureAgent;
    private float alertnessRadius = 12f;
    private float distanceToPlayer;
    public AurafoxSleepingState(StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, GameObject player) : base(stateMachine)
    {
        this.creature = creature;
        this.creatureAgent = creatureAgent;
        this.player = player; 
    }

    public override void Enter()
    {
        creatureAgent.GetComponent<SpriteRenderer>().flipX = true;
    }
    public override void Perform()
    {
        distanceToPlayer = Vector3.Distance(creatureAgent.transform.position, player.transform.position);
        if (distanceToPlayer <= alertnessRadius && !PlayerController.Instance.isCrouching)
        {
            stateMachine.ChangeState("AurafoxAwakeState");
        }
    }
    public override void Exit()
    {
        creatureAgent.GetComponent<SpriteRenderer>().flipX = false;
    }
}