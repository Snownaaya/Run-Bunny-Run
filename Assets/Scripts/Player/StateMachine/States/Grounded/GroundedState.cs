using UnityEngine;
using UnityEngine.InputSystem;

public abstract class GroundedState : MovementState
{
    private GroundCheck _groundCheck;
    private Vector2 _swipeStart;

    public GroundedState(ISwitcher switcher, StateMachineData data, Character character, IInputProvider inputProvider)
        : base(switcher, data, character, inputProvider) =>
        _groundCheck = character.GroundCheck;

    public override void Enter()
    {
        base.Enter();

        if (InputProvider is KeyboardInputProvider)
            PlayerInput.CharacterPC.JumpPC.performed += OnJumpKeyPressed;
        else if (InputProvider is TouchInputProvider)
        {
            PlayerInput.CharacterTouch.JumpSwipe.started += OnSwipeJump;
            PlayerInput.CharacterTouch.JumpSwipe.canceled += OnSwipeJump;
        }

        CharacterView.StartGrounded();
    }

    public override void Exit()
    {
        base.Exit();
        if (InputProvider is KeyboardInputProvider)
            PlayerInput.CharacterPC.JumpPC.performed -= OnJumpKeyPressed;
        else if (InputProvider is TouchInputProvider)
        {
            PlayerInput.CharacterTouch.JumpSwipe.started -= OnSwipeJump;
            PlayerInput.CharacterTouch.JumpSwipe.canceled -= OnSwipeJump;
        }
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

    private void OnSwipeJump(InputAction.CallbackContext context)
    {
        Vector2 swipeDelta = context.ReadValue<Vector2>();

        if (swipeDelta.y < 50f && Mathf.Abs(swipeDelta.x) < 50f)
            Switcher.SwitchState<JumpingState>();
    }

    private void OnJumpKeyPressed(InputAction.CallbackContext context) =>
        Switcher.SwitchState<JumpingState>();
}