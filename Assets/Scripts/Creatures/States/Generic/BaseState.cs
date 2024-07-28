using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseState {
  
     protected StateMachine stateMachine;

     public BaseState(StateMachine stateMachine)
     {
        this.stateMachine = stateMachine;
     }

    // start
    public abstract void Enter();

    // update
    public abstract void Perform();

    // teardown
    public abstract void Exit();
}