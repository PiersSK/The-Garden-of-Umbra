using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public int waypointIndex;
    public float waitTimer;
    public override void Enter()
    {
        stateName = "PatrolState";
       PatrolCycle();
    }

    public override void Perform()
    {
        PatrolCycle();

    }
    public override void Exit()
    {

    }

    public void PatrolCycle()
    {

    }
}
