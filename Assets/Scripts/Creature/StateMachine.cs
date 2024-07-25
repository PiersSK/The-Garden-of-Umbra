using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;

    public void Initialise()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }
    public void ChangeState(BaseState newState)
    {
        if(activeState != null)
        {
            activeState.Exit();
        }

        activeState = newState;

        if (activeState != null)
        {
            activeState.stateMachine = this;
            //activeState.creature = GetComponent<Creatures>();
            activeState.Enter();
        }
    }

    public void SetDefaultState(string stateName)
    {
        Type stateType = Type.GetType(stateName);
        BaseState defaultState = (BaseState)Activator.CreateInstance(stateType);
        ChangeState(defaultState);
    }
}
