using UnityEngine;
using UnityEngine.AI;
public class DragonpuppyDashState : BaseState
{
    public float wanderRadius = 15f;
    public Vector3 randomPosition = Vector3.zero;
    private Creatures creature;
    private GameObject player;
    private NavMeshAgent creatureAgent;
    float wanderTime = 5f;
    float wanderingTime;

    public DragonpuppyDashState(StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, float wanderRadius) : base(stateMachine)
    {
        this.creature = creature;
        this.wanderRadius = wanderRadius;
        this.creatureAgent = creatureAgent;
    }

    public override void Enter()
    {
        creatureAgent.SetDestination(GetNewDestination());
    }
    

    public override void Perform()
    {
       Wander();
    }
    
    public override void Exit()
    {

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
        if(Vector3.Distance(creatureAgent.transform.position, creatureAgent.destination) < creatureAgent.stoppingDistance)
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