using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class GreatOakWyrmIdleState : BaseState
{
    private NavMeshAgent creatureAgent;
    private Creatures creature;
    private GameObject player;
    private float followRadius = 3f;

    public GreatOakWyrmIdleState(StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, GameObject player) : base(stateMachine)
    {
        this.creatureAgent = creatureAgent;
        this.creature = creature;
        this.player = player;
    }

    public override void Enter()
    {
        creatureAgent.GetComponent<GreatOakWyrm>().isUnderTree = true;
        creatureAgent.SetDestination(creature.spawnPoint);
        creatureAgent.GetComponent<GreatOakWyrm>().surpriseMarker.SetActive(false);
    }
    public override void Exit()
    {
        creatureAgent.speed = 1f;
    }
    public override void Perform()
    {
        if(Vector3.Distance(creatureAgent.transform.position, player.transform.position) < followRadius)
        {
            stateMachine.ChangeState("GreatOakWyrmFollowState");
        }
    }
}
        