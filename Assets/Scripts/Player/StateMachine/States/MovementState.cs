using UnityEngine;

public abstract class MovementState : IStates
{
    public void Enter() =>
        Debug.Log(GetType());

    public void Exit() { }
}