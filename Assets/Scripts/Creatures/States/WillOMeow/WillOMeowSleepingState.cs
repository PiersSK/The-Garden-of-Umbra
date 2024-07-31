using UnityEngine.AI;
using UnityEngine;

public class WillOMeowSleepingState : BaseState
{
    private Creatures creature;
    private GameObject player;
    private NavMeshAgent creatureAgent;
    private FoodBowl foodBowl;
    private float eatingTimer;
    private float maxEatingTime = 7f;

    public WillOMeowSleepingState(StateMachine stateMachine, NavMeshAgent creatureAgent, Creatures creature, GameObject player) : base(stateMachine)
    {
        this.creatureAgent = creatureAgent;
        this.creature = creature;
        this.player = player;

        eatingTimer = 0;
        foodBowl = GameObject.Find("FoodBowl").GetComponent<FoodBowl>();
    }

    public override void Enter()
    {
        creatureAgent.GetComponent<Animator>().SetBool("IsWalking", false);
        creatureAgent.GetComponent<Animator>().SetBool("Appeared", true);

        //creatureAgent.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        creatureAgent.GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        eatingTimer = 0;
    }

    public override void Exit()
    {
        creatureAgent.GetComponent<Animator>().SetBool("IsWalking", true);
        creatureAgent.GetComponent<Animator>().SetBool("Appeared", false);
        creatureAgent.GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;


    }

    public override void Perform()
    {   
        eatingTimer += Time.deltaTime;

        if(eatingTimer >= maxEatingTime)
        {
            foodBowl.FoodEaten();
            stateMachine.ChangeState("WillOMeowWanderingState");
        }
    }
}