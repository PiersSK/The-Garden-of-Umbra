using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AurafoxAwakeState : BaseState
{
    private Creatures creature;
    private GameObject player;
    private NavMeshAgent creatureAgent;
    private float teleportRadius = 6f;
    private float distanceToPlayer;
    private float detectionWindow = 2f;
    private float alertnessRadius = 12f;
    public float elapsedTime;


    public AurafoxAwakeState(StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, GameObject player) : base(stateMachine)
    {
        this.creatureAgent = creatureAgent;
        this.player = player;
        this.creature = creature;
    }

    public override void Enter()
    {
       // creatureAgent.GetComponent<Animator>().SetBool("IsAwake", true);
        creatureAgent.GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        creatureAgent.GetComponent<Aurafox>().surpriseMarker.SetActive(true);
        elapsedTime = 0f;
    }
    public override void Perform()
    {
        distanceToPlayer = Vector3.Distance(creatureAgent.transform.position, player.transform.position);
        elapsedTime+=Time.deltaTime;
        if (elapsedTime < detectionWindow)
        {
            if (PlayerController.Instance.isCrouching)
            {
            stateMachine.ChangeState("AurafoxSleepingState");
            }
        }
        if (distanceToPlayer <= teleportRadius)
        {   
                stateMachine.ChangeState("AurafoxTeleportState");
        }
        if (distanceToPlayer >= alertnessRadius)
        {
                stateMachine.ChangeState("AurafoxSleepingState");
        }

    }
    public override void Exit()
    {
        creatureAgent.GetComponent<Aurafox>().surpriseMarker.SetActive(false);
    }
}