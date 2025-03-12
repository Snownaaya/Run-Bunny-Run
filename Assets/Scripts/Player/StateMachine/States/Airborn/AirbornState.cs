using UnityEngine;

public abstract class AirbornState : MovementState
{
    private readonly AirbornStateConfig _airbornConfig;

    public AirbornState(ISwitcher switcher, StateMachineData data, Character character, IInputProvider inputProvider) : base(switcher, data, character, inputProvider) =>
        _airbornConfig = character.CharacterConfig.AirbornConfig;

    public override void Enter()
    {
        base.Enter();

        Data.Speed = _airbornConfig.Speed;
        CharacterView.StartAirborne();
    }

    public override void Exit()
    {
        base.Exit();
        CharacterView.StopAirborne();
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (InputProvider.MoveDown?.WasPressedThisFrame() == true)
            Data.YVelocity -= _airbornConfig.MoveDown;

        Data.Speed = _airbornConfig.HorizontalSpeed;
    }

    public override void Update()
    {
        base.Update();

        Data.YVelocity -= _airbornConfig.BaseGravity * Time.deltaTime;
    }
}