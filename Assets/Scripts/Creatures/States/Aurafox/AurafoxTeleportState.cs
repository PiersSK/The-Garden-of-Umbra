using UnityEngine;
using UnityEngine.AI;

public class AurafoxTeleportState : BaseState 
{
    private NavMeshAgent creatureAgent;
    private Creatures creature;
    private GameObject player;
    private Vector3 teleportPosition = new Vector3(18.99f, 1.5f, -2.88f);
    public AurafoxTeleportState (StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, GameObject player) : base (stateMachine)
    {
        this.creatureAgent = creatureAgent;
        this.creature = creature;
        this.player = player;
    }

    public override void Enter()
    {
        //creatureAgent.GetComponent<Animator>().SetBool("IsAwake", true);
        player.gameObject.SetActive(false);
        player.transform.position = teleportPosition;        
        stateMachine.ChangeState("AurafoxSleepingState");
        player.gameObject.SetActive(true);
    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
    }
}

