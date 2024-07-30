using UnityEngine.AI;
using UnityEngine;
using System.Collections.Generic;


public class WillOMeowWanderingState : BaseState
{

    private NavMeshAgent creatureAgent;
    private Creatures creature;
    private GameObject player;
    private GameObject path;
    private float alertnessRadius = 8f;
    private float wanderRadius = 15f;
    float wanderTime = 5f;
    float wanderingTime;
    GameObject foodBowl;

    List<Vector3> waypoints = new List<Vector3>();
    int waypointIndex;

    public WillOMeowWanderingState(StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, GameObject player, float wanderRadius) : base(stateMachine)
    {
        this.creatureAgent = creatureAgent;
        this.creature = creature;
        this.player = player;
        this.wanderRadius = wanderRadius;
        path = GameObject.Find(creature.pathName);
        foodBowl = GameObject.Find("FoodBowl");

        foreach (Transform child in path.transform)
        {
            waypoints.Add(child.position);
        }
        waypoints.Add(new Vector3(foodBowl.transform.position.x - 1f, foodBowl.transform.position.y, foodBowl.transform.position.z));
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

        Vector3 currPos = creatureAgent.transform.position;

        if (currPos.x == waypoints[3].x && currPos.z == waypoints[3].z)
        {
            creatureAgent.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            stateMachine.ChangeState("WillOMeowSleepingState");
        }
    }

    public Vector3 GetNewDestination()
    {
        Vector3 newDestination = Vector3.zero;
        NavMeshHit hit;
        bool notInObstacle = false;

        if (foodBowl.GetComponent<FoodBowl>().foodInBowl)
        {
            return waypoints[3];
        }

        if (waypointIndex >= waypoints.Count)
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

