using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class GreatOakWyrmFollowState : BaseState
{

    public NavMeshAgent creatureAgent;
    public Creatures creature;
    public GameObject player;
    private SpriteRenderer spriteRenderer;
    public float treeShadowRadius = 13f;
    private float followRadius = 3f;
    
    public GreatOakWyrmFollowState(StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, GameObject player, SpriteRenderer spriteRenderer) : base(stateMachine)
    {
        this.creatureAgent = creatureAgent;
        this.creature = creature;
        this.player = player;
        this.spriteRenderer = spriteRenderer;
    }

    public override void Enter()
    {
        Debug.Log("Follow State Entered");
        creatureAgent.speed = 1f;
    }
    public override void Exit()
    {
        
    }
    public override void Perform()
    {
        creatureAgent.SetDestination(player.transform.position);
        if (Vector3.Distance(creatureAgent.transform.position, player.transform.position) >= followRadius)
        {
            stateMachine.ChangeState("GreatOakWyrmIdleState");
        }
        if (Vector3.Distance(creatureAgent.transform.position, creature.spawnPoint) >= treeShadowRadius)
        {
            spriteRenderer.shadowCastingMode = ShadowCastingMode.On;
        }

    }
}