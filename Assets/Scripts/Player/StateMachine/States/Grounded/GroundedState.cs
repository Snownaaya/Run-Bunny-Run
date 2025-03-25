public abstract class GroundedState : MovementState
{
    private GroundCheck _groundCheck;

    public GroundedState(ISwitcher switcher, StateMachineData data, Character character, IInputProvider inputProvider)
        : base(switcher, data, character, inputProvider) =>
        _groundCheck = character.GroundCheck;

    public override void Enter()
    {
        base.Enter();

        InputProvider.JumpPressed += OnJump;

        CharacterView.StartGrounded();
    }

    public override void Exit()
    {
        base.Exit();

        InputProvider.JumpPressed -= OnJump;

        CharacterView.StopGrounded();
    }

    public override void Update()
    {
        base.Update();
        if (_groundCheck.IsTouches == false)
            Switcher.SwitchState<FallingState>();
    }

    private void OnJump()
    {
        if (_groundCheck.IsTouches)
            Switcher.SwitchState<JumpingState>();
    }
}