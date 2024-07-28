
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CottonSpriteWanderState : BaseState
{
    public float wanderRadius;
    public Vector3 randomPosition = Vector3.zero;
    private Creatures creature;
    private GameObject player;
    private NavMeshAgent creatureAgent;

    public CottonSpriteWanderState(StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, float wanderRadius, GameObject player) : base(stateMachine)
    {
        this.creature = creature;
        this.player = player;
        this.wanderRadius = wanderRadius;
        this.creatureAgent = creatureAgent;
    }

    public override void Enter()
    {
        creatureAgent.stoppingDistance = 1f;
        creatureAgent.SetDestination(GetNewDestination());
        creatureAgent.GetComponent<Animator>().SetBool("IsWalking", true);
    }
    

    public override void Perform()
    {
        Wander();
        if (creatureAgent.velocity.x > 0)
            creatureAgent.GetComponent<SpriteRenderer>().flipX = false;
        else if (creatureAgent.velocity.x < 0)
            creatureAgent.GetComponent<SpriteRenderer>().flipX = true;

        if (Vector3.Distance(creatureAgent.transform.position, player.transform.position) < 5f)
        {
            stateMachine.ChangeState("CottonSpriteFollowState");
        }
    }
    
    public override void Exit()
    {
        creatureAgent.GetComponent<Animator>().SetBool("IsWalking", false);
    }

    public Vector3 GetNewDestination()
    {
        Vector3 newDestination = Vector3.zero;
        NavMeshHit hit;
        bool notInObstacle = false;
        for (int i = 0; i < 10; i++)
        {
            newDestination = new Vector3(
                creature.spawnPoint.x + Random.Range(0f, wanderRadius),
                creature.spawnPoint.y,
                creature.spawnPoint.z + Random.Range(0f, wanderRadius)) ;

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
        if(Vector3.Distance(creatureAgent.transform.position, creatureAgent.destination) < (creatureAgent.stoppingDistance*1.5f))
        {
            Debug.Log("Sitting");
            stateMachine.ChangeState("CottonSpriteSittingState");
        }

    }
}