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
        Debug.Log("Entered Idling State");
        creatureAgent.speed = 10f;
        creatureAgent.SetDestination(creature.spawnPoint);
    }
    public override void Exit()
    {}
    public override void Perform()
    {
        if(Vector3.Distance(creatureAgent.transform.position, player.transform.position) < followRadius)
        {
            stateMachine.ChangeState("GreatOakWyrmFollowState");
        }
    }
}
        