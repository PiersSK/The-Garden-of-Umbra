using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DespawnedState : BaseState
{
    // Start is called before the first frame update
    public override void Enter()
    {
        
    } 

    public override void Perform()
    {
        if(creature.isActiveAndEnabled)
        {
            creature.DespawnCreature();
        }
    }

    // Update is called once per frame
    public override void Exit()
    {

    }
}
