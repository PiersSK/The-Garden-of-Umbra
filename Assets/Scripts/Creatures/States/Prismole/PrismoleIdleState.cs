using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class PrismoleIdleState : BaseState
{

    private NavMeshAgent creatureAgent;
    private Creatures creature;
    private GameObject player;
    private Vector3 waypoint3 = new Vector3(584.32f, 0.24f, -132.74f);
    private List<Vector3> waypoints = new List<Vector3>();
    private float detectionRadius = 5f;
    private float outerDetectionRadius = 8f;
    public PrismoleIdleState(StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, GameObject player) : base(stateMachine)
    {
        this.creatureAgent = creatureAgent;
        this.creature = creature;
        this.player = player;
    }

    public override void Enter()
    {
        creatureAgent.GetComponent<Prismole>().surpriseMarker.SetActive(false);
        if(Vector3.Distance(creatureAgent.transform.position, waypoint3) < detectionRadius)
        {
            creatureAgent.GetComponent<Prismole>().isInLight = true;
        }
    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
        if(Vector3.Distance(creatureAgent.transform.position, player.transform.position) <= outerDetectionRadius && !PlayerController.Instance.isCrouching)
        {
            creatureAgent.GetComponent<Prismole>().surpriseMarker.SetActive(true);

        }
        if(Vector3.Distance(creatureAgent.transform.position, player.transform.position) <= detectionRadius && !PlayerController.Instance.isCrouching)
        {
            creatureAgent.GetComponent<Prismole>().OnTeleport();
            stateMachine.ChangeState("PrismoleFleeState");
        }
    }
}