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
        Debug.Log("I'm awake now");
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
            Debug.Log("Watching you squat really makes me sleepy");
            stateMachine.ChangeState("AurafoxSleepingState");
            }
        }
        if (distanceToPlayer <= teleportRadius)
        {   
                Debug.Log("Get yeeted, son.");
                stateMachine.ChangeState("AurafoxTeleportState");
        }
        if (distanceToPlayer >= alertnessRadius)
        {
                Debug.Log("Bye den, bedtime for meeeee");
                stateMachine.ChangeState("AurafoxSleepingState");
        }

    }
    public override void Exit()
    {
        creatureAgent.GetComponent<Aurafox>().surpriseMarker.SetActive(false);
    }
}