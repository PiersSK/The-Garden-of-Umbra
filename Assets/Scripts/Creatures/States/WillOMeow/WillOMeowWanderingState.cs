using UnityEngine.AI;
using UnityEngine;
using System.Collections.Generic;


public class WillOMeowWanderingState : BaseState
{

    private NavMeshAgent creatureAgent;
    private Creatures creature;
    private GameObject player;
    private float alertnessRadius = 8f;
    private float wanderRadius = 15f;
    float wanderTime = 5f;
    float wanderingTime;

    List<Vector3> waypoints;
    int waypointIndex;

    public WillOMeowWanderingState(StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, GameObject player, float wanderRadius) : base(stateMachine)
    {
        this.creatureAgent = creatureAgent;
        this.creature = creature;
        this.player = player;
        this.wanderRadius = wanderRadius;
        foreach (GameObject waypointObj in creature.prefab.GetComponent<List<GameObject>>())
        {
            waypoints.Add(waypointObj.transform.position);
        }
        waypointIndex = 0;
    }

    public override void Enter()
    {
        creatureAgent.GetComponent<Animator>().SetBool("IsWalking", true);
        creatureAgent.SetDestination(GetNewDestination());
    }

    public override void Exit()
    {
        creatureAgent.GetComponent<Animator>().SetBool("IsWalking", false);
    }

    public override void Perform()
    {
        Wander();

        if (creatureAgent.velocity.x > 0)
            creatureAgent.GetComponent<SpriteRenderer>().flipX = false;
        else if (creatureAgent.velocity.x < 0)
            creatureAgent.GetComponent<SpriteRenderer>().flipX = true;

        creatureAgent.GetComponent<Animator>().SetBool("IsWalking", creatureAgent.velocity != Vector3.zero);

        float distanceToPlayer = Vector3.Distance(creatureAgent.transform.position, player.transform.position);
        if(distanceToPlayer <= alertnessRadius)
        {
            creatureAgent.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f, 0.3f);
        }
        else
        {
            creatureAgent.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public Vector3 GetNewDestination()
    {
        Vector3 newDestination = Vector3.zero;
        NavMeshHit hit;
        bool notInObstacle = false;

        Vector3 currPos = creatureAgent.transform.position;
        
        if(waypointIndex >= waypoints.Count)
        {
            waypointIndex = 0;
        }

        newDestination = waypoints[waypointIndex++];
        if (NavMesh.SamplePosition(newDestination, out hit, wanderRadius, NavMesh.AllAreas))
        {
                notInObstacle = true;
        }
        
        if (notInObstacle)
        {
            return newDestination;
        }
        else
        {
            return creature.spawnPoint;
        }
    }

    public void Wander()
    {
        if (Vector3.Distance(creatureAgent.transform.position, creatureAgent.destination) < creatureAgent.stoppingDistance)
        {
            creatureAgent.SetDestination(GetNewDestination());
        }
        wanderingTime += Time.deltaTime;
        if (wanderingTime > wanderTime)
        {
            creatureAgent.SetDestination(GetNewDestination());
            wanderingTime = 0;
        }

    }
}

