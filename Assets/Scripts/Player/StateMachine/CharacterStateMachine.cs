using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStateMachine : ISwitcher
{
    private readonly List<IInputState> _states;
    private readonly Character _character;

    private StateMachineData _data;
    private IInputState _currentStates;
    private IInputProvider _inputProvider;
    private Vector3 _velocity;

    public CharacterStateMachine(Character character, IInputProvider inputProvider)
    {
        _character = character;
        _inputProvider = inputProvider;

        _data = new StateMachineData();

        _states = new List<IInputState>
        {
            new IdlingState(this, _data, _character, _inputProvider),
            new RunningState(this, _data, _character, _inputProvider),
            new JumpingState(this, _data, _character, _inputProvider),
            new FallingState(this, _data, _character, _inputProvider),
        };

        _currentStates = _states[0];
        _currentStates.Enter();
    }

    public StateMachineData Data => _data;

    public void SwitchState<T>() where T : IStates
    {
        IInputState state = _states.FirstOrDefault(state => state is T);

        _currentStates?.Exit();
        _currentStates = state;
        _currentStates.Enter();
    }

    public void HandleInput() =>
        _currentStates.HandleInput();

    public void Update() =>
        _currentStates.Update();

    public void ResetVelocity() =>
        _velocity = Vector3.zero;
}