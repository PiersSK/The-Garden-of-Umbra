using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseState {
    public Creature creature;
    public StateMachine stateMachine;


    // start
    public abstract void Enter();

    // update
    public abstract void Perform();

    // teardown
    public abstract void Exit();
}