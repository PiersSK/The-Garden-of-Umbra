using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class CottonSpriteSittingState : BaseState
{
    public float sittingTime = 5f;
    public float timeSat = 0f;
    private Creatures creature;
    private GameObject player;
    private NavMeshAgent creatureAgent;
    public CottonSpriteSittingState(StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, GameObject player) : base(stateMachine)
    {
        this.creature = creature;
        this.player = player;
        this.creatureAgent = creatureAgent;
    }
    public override void Enter()
    {
    }

    public override void Perform()
    {
       Sitting();
    }
    
    public override void Exit()
    {
        timeSat = 0f;
    }
    
    public void Sitting()
    {
        timeSat += Time.deltaTime;
       if (timeSat >= sittingTime && Vector3.Distance(creatureAgent.transform.position, player.transform.position) > 5f)
       {
            stateMachine.ChangeState("CottonSpriteWanderState");
       }
       if (Vector3.Distance(creatureAgent.transform.position, player.transform.position) < 5f && Vector3.Distance(creatureAgent.transform.position, player.transform.position) > 3f)
       {
            stateMachine.ChangeState("CottonSpriteFollowState");
       }
    }
        
    
}
