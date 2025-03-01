using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerMovement : IPlayerAction
{
    private Vector2 _touchpadMoveDirection;
    private IMoveble _moveble;
    private Finger _movementFinger;
    private Vector2 _startTouchPosition;

    private float _sensitivity = 15f;

    public PlayerMovement(IMoveble moveble)
    {
        _moveble = moveble;
        _touchpadMoveDirection = Vector2.zero;
    }

    public void Enable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += OnFingerDown;
        ETouch.Touch.onFingerMove += OnFingerMove;
        ETouch.Touch.onFingerUp += OnFingerUp;
    }

    public void Disable()
    {
        ETouch.Touch.onFingerDown -= OnFingerDown;
        ETouch.Touch.onFingerMove -= OnFingerMove;
        ETouch.Touch.onFingerUp -= OnFingerUp;
        EnhancedTouchSupport.Disable();
    }

    public void Move()
    {
        Vector3 scaleMovement = _moveble.Speed * Time.deltaTime * new Vector3(_touchpadMoveDirection.x, 0,
            _touchpadMoveDirection.y);
        _moveble.Transform.Translate(scaleMovement);
    }

    public void OnFingerDown(Finger touchFinger)
    {
        if (_movementFinger == null)
        {
            _movementFinger = touchFinger;
            _startTouchPosition = touchFinger.currentTouch.screenPosition;
            _touchpadMoveDirection = Vector2.zero;
        }
    }

    public void OnFingerMove(Finger touchFinger)
    {
        if (_movementFinger == touchFinger)
        {
            Vector2 currentPos = touchFinger.currentTouch.screenPosition;

            float deltaX = currentPos.x - _startTouchPosition.x;

            _touchpadMoveDirection = new Vector2(deltaX, 0).normalized * _sensitivity;
        }
    }

    public void OnFingerUp(Finger finger)
    {
        if (_movementFinger == finger)
        {
            _movementFinger = null;
            _touchpadMoveDirection = Vector2.zero;
        }
    }
}