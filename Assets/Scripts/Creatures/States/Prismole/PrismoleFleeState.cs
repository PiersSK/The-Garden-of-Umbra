
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrismoleFleeState : BaseState 
{
    private NavMeshAgent creatureAgent;
    private Creatures creature;
    private GameObject player;
    private int waypointIndex;
    private List<Vector3> waypoints = new List<Vector3>();
    private Vector3 waypoint1 = new Vector3(585.897f, 0.24f, -108.638f);
    private Vector3 waypoint2 = new Vector3(568.98f, 2.48241f, -121.92f);
    private Vector3 waypoint3 = new Vector3(584.32f, 0.24f, -132.74f);
    private float detectionRadius = 5f;
    public PrismoleFleeState(StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, GameObject player) : base (stateMachine)
    {
        this.creatureAgent = creatureAgent;
        this.creature = creature;
        this.player = player;
        waypoints.Add(waypoint1);
        waypoints.Add(waypoint2);
        waypoints.Add(waypoint3);
    }

    public override void Enter()
    {
        creatureAgent.GetComponent<Prismole>().isInLight = false;
        creatureAgent.GetComponent<Prismole>().OnTeleport();
    }

    public override void Perform()
    {
        if(!CanDetectPlayer())
        {
            stateMachine.ChangeState("PrismoleIdleState");
        }
        else
        {
            TeleportToNewPosition();
        }
    }

    public override void Exit()
    {
        creatureAgent.GetComponent<Prismole>().OnTeleport();
    }

    public void TeleportToNewPosition()
    {
        Vector3 closestWaypoint = waypoints[0];
        float distanceToClosest = Vector3.Distance(creatureAgent.transform.position, closestWaypoint);
        for (int i=1; i < waypoints.Count; i++)
        {
            float distance = Vector3.Distance(waypoints[i], creatureAgent.transform.position);
            if(distance <= distanceToClosest)
            {
                distanceToClosest = distance;
                closestWaypoint = waypoints[i];
            }
        }
        waypoints.Remove(closestWaypoint);
        waypointIndex = Random.Range(0, waypoints.Count);
        creatureAgent.transform.position = waypoints[waypointIndex];
        waypoints.Add(closestWaypoint);
    }

    public bool CanDetectPlayer()
    {
        if (Vector3.Distance(creatureAgent.transform.position, player.transform.position) > detectionRadius)
        {
            return false;
        }
        return true;
    }
}