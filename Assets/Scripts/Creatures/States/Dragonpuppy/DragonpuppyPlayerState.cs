using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
public class DragonpuppyPlayerState : BaseState
{
    public Vector3 randomPosition = Vector3.zero;
    private Creatures creature;
    private GameObject player;
    private NavMeshAgent creatureAgent;
    private float wanderRadius = 15f;
    private float detectionRadius = 12f;
    private bool hasReachedPlayer = false;
    private bool hasReachedSpawn = false;

    public DragonpuppyPlayerState(StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, GameObject player) : base(stateMachine)
    {
        this.creature = creature;
        this.player = player;
        this.creatureAgent = creatureAgent;
    }

    public override void Enter()
    {
        Debug.Log("Entered Player State");
        creatureAgent.SetDestination(player.transform.position);
    }
    

    public override void Perform()
    {
        HasReachedPlayer();
        if(hasReachedPlayer)
        {
            creatureAgent.SetDestination(creature.spawnPoint);
        }
        HasReachedDestination();
        if(hasReachedSpawn)
        {
            stateMachine.ChangeState("DragonpuppyDashState");
        }

    }
    
    public override void Exit()
    {
    }
    public void HasReachedPlayer()
    {
        if (Vector3.Distance(creatureAgent.transform.position, player.transform.position) < creatureAgent.stoppingDistance)
        {
            hasReachedPlayer = true;
        }
    }

    public void HasReachedDestination()
    {
        if(Vector3.Distance(creatureAgent.transform.position, creature.spawnPoint) < creatureAgent.stoppingDistance)
        {
            hasReachedSpawn = true;
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
}