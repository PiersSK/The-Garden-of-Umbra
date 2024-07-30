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
    private float initialSpeed = 7f;

    public DragonpuppyPlayerState(StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, GameObject player) : base(stateMachine)
    {
        this.creature = creature;
        this.player = player;
        this.creatureAgent = creatureAgent;
    }

    public override void Enter()
    {
        creatureAgent.SetDestination(player.transform.position);
        creatureAgent.GetComponent<Dragonpuppy>().curiousMarker.SetActive(true);
        initialSpeed = creatureAgent.speed;
        creatureAgent.speed = 3f;
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
        creatureAgent.speed = initialSpeed;

        hasReachedPlayer = false;

    }

    public void HasReachedPlayer()
    {
        if(!hasReachedPlayer) { 
            creatureAgent.SetDestination(player.transform.position);
            if (Vector3.Distance(creatureAgent.transform.position, player.transform.position) < creatureAgent.stoppingDistance)
            {
                creatureAgent.SetDestination(creature.spawnPoint);
                hasReachedPlayer = true;
                creatureAgent.GetComponent<Dragonpuppy>().curiousMarker.SetActive(false);
                creatureAgent.speed = initialSpeed * 1.5f;
            }
        }
    }

    public void HasReachedDestination()
    {
        if(Vector3.Distance(creatureAgent.transform.position, creature.spawnPoint) < creatureAgent.stoppingDistance && hasReachedPlayer)
        {
            stateMachine.ChangeState("DragonpuppyDashState");
        }
    }

}