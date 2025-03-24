using UnityEngine;
using UnityEngine.InputSystem;

public class RunningState : GroundedState
{
    private RunningStateConfig _config;

    public RunningState(ISwitcher switcher, StateMachineData data, Character character, IInputProvider inputProvider) : base(switcher, data, character, inputProvider)
    {
        _config = character.CharacterConfig.RunningConfig;
    }

    public override void Enter()
    {
        base.Enter();
        Data.Speed = _config.Speed;

        CharacterView.StartRunning();
    }

    public override void Exit()
    {
        base.Exit();

        CharacterView.StopRunning();
    }

    public override void Update()
    {
        base.Update();

        if (IsHorizontalInputZero())
            Switcher.SwitchState<IdlingState>();
    }
}