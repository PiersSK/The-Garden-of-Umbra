using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HedgedogSleepingState : BaseState
{
    private NavMeshAgent creatureAgent;
    private Creatures creature;
    private GameObject player;

    private float alertnessRadius = 8f;

    public HedgedogSleepingState(StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, GameObject player) : base(stateMachine)
    {
        this.creatureAgent = creatureAgent;
        this.creature = creature;
        this.player = player;
    }

    public override void Enter()
    {
        Debug.Log("Entered Sleep");

        //animator sleeping
    }

    public override void Exit()
    {
        //animator not sleeping
    }

    public override void Perform()
    {
        float distanceToPlayer = Vector3.Distance(creatureAgent.transform.position, player.transform.position);
        if(distanceToPlayer <= alertnessRadius && !PlayerController.Instance.isCrouching)
        {
            creatureAgent.GetComponent<Hedgedog>().surpriseMarker.SetActive(true);
            stateMachine.ChangeState("HedgedogFleeState");
        }

    }
}
