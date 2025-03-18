using UnityEngine;
using UnityEngine.InputSystem;

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
        if (InputProvider is KeyboardInputProvider)
            PlayerInput.CharacterPC.MoveDownPC.performed += OnMoveDownPressed;
        else if (InputProvider is TouchInputProvider)
        {
            PlayerInput.CharacterTouch.DownSwipe.started += OnSwipeDown;
            PlayerInput.CharacterTouch.DownSwipe.canceled += OnSwipeDown;
        }
        Data.Speed = _airbornConfig.Speed;
        CharacterView.StartAirborne();
    }

    public override void Exit()
    {
        base.Exit();

        if (InputProvider is KeyboardInputProvider)
            PlayerInput.CharacterPC.MoveDownPC.performed -= OnMoveDownPressed;
        else if (InputProvider is TouchInputProvider)
        {
            PlayerInput.CharacterTouch.DownSwipe.started -= OnSwipeDown;
            PlayerInput.CharacterTouch.DownSwipe.canceled -= OnSwipeDown;
        }

        CharacterView.StopAirborne();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        Data.Speed = _airbornConfig.HorizontalSpeed * 2;
    }

    public override void Update()
    {
        base.Update();
        Data.YVelocity -= _airbornConfig.BaseGravity * Time.deltaTime;
    }

    private void OnMoveDownPressed(InputAction.CallbackContext context) =>
        Data.YVelocity -= _airbornConfig.MoveDown;

    private void OnSwipeDown(InputAction.CallbackContext context)
    {
        Vector2 swipeDelta = context.ReadValue<Vector2>();

        if (swipeDelta.y > -50f && Mathf.Abs(swipeDelta.x) < 50f)
            Data.YVelocity -= _airbornConfig.MoveDown;
    }
}