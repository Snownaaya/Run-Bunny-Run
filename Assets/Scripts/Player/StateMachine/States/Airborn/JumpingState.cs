public class JumpingState : AirbornState
{
    private JumpingStateConfig _jumpingConfig;

    public JumpingState(ISwitcher switcher, StateMachineData data, Character character, IInputProvider inputProvider) : base(switcher, data, character, inputProvider) =>
        _jumpingConfig = character.CharacterConfig.AirbornConfig.JumpingConfig;

    public override void Enter()
    {
        base.Enter();

        Data.YVelocity = _jumpingConfig.StartYVelocity;
        PlayerAudio.PlayJumpSound();
        CharacterView.StartJumping();
    }

    public override void Exit()
    {
        base.Exit();

        CharacterView.StopJumping();
    }

    public override void Update()
    {
        base.Update();

        if (Data.YVelocity <= 0)
            Switcher.SwitchState<FallingState>();
    }
}