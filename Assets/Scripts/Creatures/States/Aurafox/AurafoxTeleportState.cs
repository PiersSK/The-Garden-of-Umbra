using UnityEngine;
using UnityEngine.AI;

public class AurafoxTeleportState : BaseState 
{
    private NavMeshAgent creatureAgent;
    private Creatures creature;
    private GameObject player;

    private float teleportTimer = 0f;
    private float teleportLength = 2f;
    private Vector3 teleportPosition = new Vector3(18.99f, 1.5f, -2.88f);



    public AurafoxTeleportState (StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, GameObject player) : base (stateMachine)
    {
        this.creatureAgent = creatureAgent;
        this.creature = creature;
        this.player = player;
    }

    public override void Enter()
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<PlayerController>().playerAnimator.SetBool("IsWalking", false);
        creatureAgent.GetComponent<Animator>().SetBool("Teleporting", true);
        UIManager.Instance.SetUIForPreset(UIManager.UIPreset.Cutscene);
    }

    public override void Exit()
    {
        teleportTimer = 0f;
        player.GetComponent<CharacterController>().enabled = true;
        creatureAgent.GetComponent<Animator>().SetBool("Teleporting", false);
        UIManager.Instance.SetUIForPreset(UIManager.UIPreset.Garden);

        if(!creatureAgent.GetComponent<Aurafox>().hasTeleportedPlayer)
        {
            PlayerThoughts.Instance.ShowThought("The inhabitants of the well better be paying that Aurafox for his guard duty! Better sneak up next time...", 4f);
            creatureAgent.GetComponent<Aurafox>().hasTeleportedPlayer = true;
        }

    }

    public override void Perform()
    {
        teleportTimer += Time.deltaTime;
        if (teleportTimer >= teleportLength)
        {
            player.GetComponent<PlayerController>().smokeAnim.SetTrigger("Puff");
            player.transform.position = teleportPosition;
            stateMachine.ChangeState("AurafoxSleepingState");
        }
    }
}

