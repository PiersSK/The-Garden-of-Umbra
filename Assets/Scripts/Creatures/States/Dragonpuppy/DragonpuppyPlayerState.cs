using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
public class DragonpuppyPlayerState : BaseState
{
    public Vector3 randomPosition = Vector3.zero;
    private Creatures creature;
    private GameObject player;
    private NavMeshAgent creatureAgent;

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
        creatureAgent.GetComponent<Dragonpuppy>().curiousMarker.SetActive(true);
    }
    

    public override void Perform()
    {
        if (creatureAgent.velocity.x > 0)
            creatureAgent.GetComponent<SpriteRenderer>().flipX = true;
        else if (creatureAgent.velocity.x < 0)
            creatureAgent.GetComponent<SpriteRenderer>().flipX = false;

        creatureAgent.GetComponent<Animator>().SetBool("IsWalking", creatureAgent.velocity != Vector3.zero);

        HasReachedPlayer();
        HasReachedDestination();

    }
    
    public override void Exit()
    {
        hasReachedPlayer = false;
        hasReachedSpawn = false;

        Debug.Log("Exited Player State");

    }

    public void HasReachedPlayer()
    {
        if(!hasReachedPlayer) { 
            creatureAgent.SetDestination(player.transform.position);
            if (Vector3.Distance(creatureAgent.transform.position, player.transform.position) < creatureAgent.stoppingDistance)
            {
                Debug.Log("Reached Player");
                creatureAgent.SetDestination(creature.spawnPoint);
                hasReachedPlayer = true;
                creatureAgent.GetComponent<Dragonpuppy>().curiousMarker.SetActive(false);

            }
        }
    }

    public void HasReachedDestination()
    {
        if(Vector3.Distance(creatureAgent.transform.position, creature.spawnPoint) < creatureAgent.stoppingDistance && hasReachedPlayer)
        {
            Debug.Log("Reached Spawn");
            stateMachine.ChangeState("DragonpuppyDashState");
            hasReachedSpawn = true;
        }
    }

}