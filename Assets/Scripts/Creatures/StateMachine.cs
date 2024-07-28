using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    private Dictionary<string, BaseState> states = new Dictionary<string, BaseState>();
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

    public void AddState(string stateName, BaseState state)
    {
        states[stateName] = state;
    }
    public void ChangeState(string newStateName)
    {
        if(activeState != null)
        {
            activeState.Exit();
        }

        if (states.ContainsKey(newStateName))
        {
            activeState = states[newStateName];
            activeState.Enter();
        }
    }

    public void SetDefaultState(string defaultStateName)
    {
        if (states.ContainsKey(defaultStateName))
        {
            activeState = states[defaultStateName];
            activeState.Enter();
        }
    }
}
