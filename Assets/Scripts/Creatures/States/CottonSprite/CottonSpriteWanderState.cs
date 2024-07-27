
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CottonSpriteWanderState : BaseState
{
    public float wanderRadius = 15f;
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
            Vector2 wanderDirection = Random.insideUnitCircle * wanderRadius;
            newDestination = new Vector3(creatureAgent.transform.position.x + wanderDirection.x, creatureAgent.transform.position.y, creatureAgent.transform.position.z + wanderDirection.y);

            if (NavMesh.SamplePosition(newDestination, out hit, wanderRadius, NavMesh.AllAreas))
            {
                notInObstacle = true;
                break;
            }
        
        }
        if (notInObstacle)
        {
            Debug.Log(newDestination);
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
            stateMachine.ChangeState("CottonSpriteSittingState");
        }

    }
}