using UnityEngine.InputSystem;

public abstract class GroundedState : MovementState
{
    private GroundCheck _groundCheck;

    public GroundedState(ISwitcher switcher, StateMachineData data, Character character, IInputProvider inputProvider) : base(switcher, data, character, inputProvider) =>
        _groundCheck = character.GroundCheck;

    public override void Enter()
    {   
        base.Enter();

        PlayerInput.Character.JumpPC.started += OnJumpKeyPressed;
        PlayerInput.Character.SwipeJump.started += OnJumpKeyPressed;
        CharacterView.StartGrounded();
    }

    public override void Exit()
    {
        base.Exit();

        PlayerInput.Character.JumpPC.started -= OnJumpKeyPressed;
        PlayerInput.Character.SwipeJump.started -= OnJumpKeyPressed;
        CharacterView.StopGrounded();
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void Update()
    {
        base.Update();

        if (_groundCheck.IsTouches == false)
            Switcher.SwitchState<FallingState>();
    }

    private void OnJumpKeyPressed(InputAction.CallbackContext context) =>
        Switcher.SwitchState<JumpingState>();
}