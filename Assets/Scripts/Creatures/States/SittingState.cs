using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class SittingState : BaseState
{
    public float sittingTime = 5f;
    public float timeSat = 0f;

    public SittingState(StateMachine stateMachine) : base(stateMachine) {}
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
       if (timeSat >= sittingTime)
       {
            stateMachine.ChangeState("WanderState");
       }
    }
        
    
}
