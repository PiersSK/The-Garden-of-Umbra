using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseState {
    public Creatures creature;
    public StateMachine stateMachine;
    public string stateName;

    // start
    public abstract void Enter();

    // update
    public abstract void Perform();

    // teardown
    public abstract void Exit();
}