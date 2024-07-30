using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class GreatOakWyrmFollowState : BaseState
{

    public NavMeshAgent creatureAgent;
    public Creatures creature;
    public GameObject player;
    public float treeShadowRadius = 13f;
    private float followRadius = 3f;
    
    public GreatOakWyrmFollowState(StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, GameObject player) : base(stateMachine)
    {
        this.creatureAgent = creatureAgent;
        this.creature = creature;
        this.player = player;
    }

    public override void Enter()
    {

    }
    public override void Exit()
    {
        creatureAgent.speed = 10f;
    }
    public override void Perform()
    {
        creatureAgent.SetDestination(player.transform.position);
        if (Vector3.Distance(creatureAgent.transform.position, player.transform.position) >= followRadius)
        {
            creatureAgent.GetComponent<GreatOakWyrm>().surpriseMarker.SetActive(true);
            stateMachine.ChangeState("GreatOakWyrmIdleState");
        }
        if (Vector3.Distance(creatureAgent.transform.position, creature.spawnPoint) >= treeShadowRadius)
        {
            creatureAgent.GetComponent<GreatOakWyrm>().isUnderTree = false;
        }

    }
}