using UnityEngine.AI;
using UnityEngine;

public class WillOMeowSleepingState : BaseState
{
    private Creatures creature;
    private GameObject player;
    private NavMeshAgent creatureAgent;

    public WillOMeowSleepingState(StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, GameObject player) : base(stateMachine)
    {
        this.creatureAgent = creatureAgent;
        this.creature = creature;
        this.player = player;
    }

    public override void Enter()
    {
        creatureAgent.GetComponent<Animator>().SetBool("IsWalking", false);
        creatureAgent.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    public override void Exit()
    {
    }

    public override void Perform()
    {   
    }
}