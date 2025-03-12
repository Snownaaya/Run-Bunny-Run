public class FallingState : AirbornState
{
    private readonly GroundCheck _groundCheck;

    public FallingState(ISwitcher switcher, StateMachineData data, Character character, IInputProvider inputProvider) : base(switcher, data, character, inputProvider) =>
        _groundCheck = character.GroundCheck;

    public override void Enter()
    {
        base.Enter();
        PlayerAudio.PlayFootStep();
        CharacterView.StartFalling();
    }

    public override void Exit()
    {
        base.Exit();
        CharacterView.StopFalling();
    }

    public override void Update()
    {
        base.Update();

        if (_groundCheck.IsTouches)
        {
            Data.YVelocity = 0;

            if (IsHorizontalInputZero())
                Switcher.SwitchState<IdlingState>();
            else
                Switcher.SwitchState<RunningState>();
        }
    }
}
