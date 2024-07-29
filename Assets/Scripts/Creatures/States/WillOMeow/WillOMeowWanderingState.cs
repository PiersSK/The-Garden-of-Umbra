using UnityEngine.AI;
using UnityEngine;

    
public class WillOMeowWanderingState : BaseState
{

    private NavMeshAgent creatureAgent;
    private Creatures creature;
    private GameObject player;
    private Vector3 randomPosition = Vector3.zero;
    private float alertnessRadius = 8f;
    private float wanderRadius = 15f;
    float wanderTime = 5f;
    float wanderingTime;

    public WillOMeowWanderingState(StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, GameObject player, float wanderRadius) : base(stateMachine)
    {
        this.creatureAgent = creatureAgent;
        this.creature = creature;
        this.player = player;
        this.wanderRadius = wanderRadius;
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
            creatureAgent.GetComponent<SpriteRenderer>().flipX = true;
        else if (creatureAgent.velocity.x < 0)
            creatureAgent.GetComponent<SpriteRenderer>().flipX = false;

        creatureAgent.GetComponent<Animator>().SetBool("IsWalking", creatureAgent.velocity != Vector3.zero);

        float distanceToPlayer = Vector3.Distance(creatureAgent.transform.position, player.transform.position);
        if(distanceToPlayer <= alertnessRadius && !PlayerController.Instance.isCrouching)
        {
            creatureAgent.GetComponent<WillOMeow>().surpriseMarker.SetActive(true);
            
        }
    }

    public Vector3 GetNewDestination()
    {
        Vector3 newDestination = Vector3.zero;
        NavMeshHit hit;
        bool notInObstacle = false;
        for (int i = 0; i < 10; i++)
        {
            Vector2 wanderDirection = Random.insideUnitCircle * wanderRadius * 10;
            newDestination = new Vector3(creatureAgent.transform.position.x + wanderDirection.x, creatureAgent.transform.position.y, creatureAgent.transform.position.z + wanderDirection.y);

            if (NavMesh.SamplePosition(newDestination, out hit, wanderRadius, NavMesh.AllAreas))
            {
                notInObstacle = true;
                break;
            }

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

