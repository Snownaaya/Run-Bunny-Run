using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine;
using System;

[Serializable]
public class PlayerJumper : IPlayerAction
{
    private LayerMask _groundMask;
    private Transform _targetPoint;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private PlayerAudio _audio;

    private IMoveble _moveble;
    private bool _isJumping;
    private float _checkRaduis;

    private Vector2 _startTouchPosition;
    private float _swipeThreshold = 100f;

    public PlayerJumper(LayerMask groundMask, Transform targetPoint, Rigidbody rigidbody, IMoveble moveble, float checkRaduis, Animator animator, PlayerAudio audio)
    {
        _groundMask = groundMask;
        _targetPoint = targetPoint;
        _rigidbody = rigidbody;
        _moveble = moveble;
        _checkRaduis = checkRaduis;
        _animator = animator;
        _audio = audio;
    }

    public void Enable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += OnFingerDown;
        ETouch.Touch.onFingerUp += OnFingerUp;
    }

    public void Disable()
    {
        ETouch.Touch.onFingerDown -= OnFingerDown;
        ETouch.Touch.onFingerUp -= OnFingerUp;
        EnhancedTouchSupport.Disable();
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * _moveble.JumpForce, ForceMode.Impulse);
            _animator.SetTrigger(AnimatorData.Parameters.Jump);
            _audio.PlayJumpSound();
        }
    }

    public bool IsGrounded() =>
        Physics.CheckSphere(_targetPoint.position, _checkRaduis, _groundMask);

    private void Fall()
    {
        if (IsGrounded() == false)
            _rigidbody.AddForce(Vector3.down * _moveble.JumpForce, ForceMode.Impulse);
    }

    private void OnFingerDown(Finger finger)
    {
        _startTouchPosition = finger.currentTouch.screenPosition;
        _isJumping = false;
    }

    private void OnFingerUp(Finger finger)
    {
        Vector2 endTouchPosition = finger.currentTouch.screenPosition;
        Vector2 swipeDelta = endTouchPosition - _startTouchPosition;

        if (swipeDelta.y > _swipeThreshold && Mathf.Abs(swipeDelta.y) > Mathf.Abs(swipeDelta.x))
        {
            _isJumping = true;
            Jump();
        }
        else if (swipeDelta.y < -_swipeThreshold && Math.Abs(swipeDelta.y) > Math.Abs(swipeDelta.x))
        {
            Fall();
        }
        else
        {
            _isJumping = false;
        }
    }
}
