using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingState : BaseState
{
    public override void Enter()
    {
        stateName = "SittingState";
        Sitting();
    }

    public override void Perform()
    {
       
    }
    
    public override void Exit()
    {

    }
    
    public void Sitting()
    {
        
    }
        
    
}
