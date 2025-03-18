using System;
using UnityEngine;

public abstract class MovementState : IStates, IInputState
{
    private IInputProvider _inputProvider;
    private ISwitcher _switcher;
    private StateMachineData _data;
    private Character _character;

    protected MovementState(ISwitcher switcher, StateMachineData data, Character character, IInputProvider inputProvider)
    {
        _switcher = switcher;
        _data = data;
        _character = character;
        _inputProvider = inputProvider;
    }

    public CharacterView CharacterView => _character.CharacterView;
    public PlayerAudio PlayerAudio => _character.PlayerAudio;
    public StateMachineData Data => _data;
    public PlayerInput PlayerInput => _character.PlayerInput;
    public ISwitcher Switcher => _switcher;
    public IInputProvider InputProvider => _inputProvider;

    public virtual void Enter() =>
        Debug.Log(GetType());

    public virtual void Exit() { }

    public virtual void HandleInput() =>
        Data.XInput = InputProvider.Move;

    public virtual void Update()
    {
        Vector3 moveDirection = new Vector3(Data.XInput, Data.YVelocity, 0f);
        _character.CharacterController.Move(moveDirection * Data.Speed * Time.deltaTime);
    }

    protected bool IsHorizontalInputZero() =>
        Data.XInput == 0;
}