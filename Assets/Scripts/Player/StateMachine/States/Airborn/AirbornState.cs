using UnityEngine;

public abstract class AirbornState : MovementState
{
    private readonly AirbornStateConfig _airbornConfig;
    private Vector2 _swipeStart;

    public AirbornState(ISwitcher switcher, StateMachineData data, Character character, IInputProvider inputProvider)
        : base(switcher, data, character, inputProvider) =>
        _airbornConfig = character.CharacterConfig.AirbornConfig;

    public override void Enter()
    {
        base.Enter();

        InputProvider.MoveDownPressed += OnMoveDown;

        Data.Speed = _airbornConfig.Speed;
        CharacterView.StartAirborne();
    }

    public override void Exit()
    {
        base.Exit();

        InputProvider.MoveDownPressed -= OnMoveDown;

        CharacterView.StopAirborne();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        Data.Speed = _airbornConfig.HorizontalSpeed * 1.5f;
    }

    public override void Update()
    {
        base.Update();
        Data.YVelocity -= _airbornConfig.BaseGravity * Time.deltaTime;
    }

    private void OnMoveDown() =>
            Data.YVelocity -= _airbornConfig.MoveDown;
}