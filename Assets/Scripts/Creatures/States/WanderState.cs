
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class WanderState : BaseState
{
    public float wanderRadius = 15f;
    public Vector3 randomPosition = Vector3.zero;
    private NavMeshAgent creatureAgent;
    private Vector3 spawnPoint;

    public WanderState(StateMachine stateMachine, Vector3 spawnPoint, NavMeshAgent creatureAgent, float wanderRadius) : base(stateMachine)
    {
        this.spawnPoint = spawnPoint;
        this.creatureAgent = creatureAgent;
        this.wanderRadius = wanderRadius;
    }

    public override void Enter()
    {
        creatureAgent.SetDestination(GetNewDestination());
        Debug.Log("current destination: " + creatureAgent.destination);
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
            return newDestination;
        }
        else
        {
            return spawnPoint;
        }
       
        
    }

    public void Wander()
    {
        if(Vector3.Distance(creatureAgent.transform.position, creatureAgent.destination) < creatureAgent.stoppingDistance)
        {
            stateMachine.ChangeState("SittingState");
        }

    }
}