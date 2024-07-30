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
    float playerCooldown = 0f;
    float wanderingTime;
    
    float detectionRadius = 12f;

    public DragonpuppyDashState(StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, float wanderRadius, GameObject player) : base(stateMachine)
    {
        this.creature = creature;
        this.wanderRadius = wanderRadius;
        this.creatureAgent = creatureAgent;
        this.player = player;
    }

    public override void Enter()
    {
        creatureAgent.GetComponent<Animator>().SetBool("IsWalking", true);
        creatureAgent.SetDestination(GetNewDestination());
    }
    

    public override void Perform()
    {
        Wander();

        if (creatureAgent.velocity.x > 0)
            creatureAgent.GetComponent<SpriteRenderer>().flipX = true;
        else if (creatureAgent.velocity.x < 0)
            creatureAgent.GetComponent<SpriteRenderer>().flipX = false;
        
        creatureAgent.GetComponent<Animator>().SetBool("IsWalking", creatureAgent.velocity != Vector3.zero);

        playerCooldown += Time.deltaTime;
        if (Vector3.Distance(creatureAgent.transform.position, player.transform.position) < detectionRadius
            && playerCooldown >= wanderingTime * 6f)
        {
            stateMachine.ChangeState("DragonpuppyPlayerState");
        }
    }
    
    public override void Exit()
    {
        playerCooldown = 0f;
        creatureAgent.GetComponent<Animator>().SetBool("IsWalking", false);
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