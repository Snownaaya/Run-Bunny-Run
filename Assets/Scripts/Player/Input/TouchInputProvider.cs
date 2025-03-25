using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class TouchInputProvider : MonoBehaviour, IInputProvider
{
    [SerializeField] private float _fingerDownDelay;
    [SerializeField] private float _fingerMinRejectX = 0.2f; // увеличить, если прыжок не срабатывает
    [SerializeField] private float _fingerMaxMoveRejectX = 0.5f;

    private Finger _currentFinger;
    private Coroutine _coroutine;
    private float _fingerDownTime;

    public event Action JumpPressed;
    public event Action MoveDownPressed;

    public float Move { get; private set; }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        EnhancedTouchSupport.Disable();
    }

    private void HandleFingerDown(Finger finger)
    {
        if (_coroutine != null)
            return;

        _fingerDownTime = 0;
        _currentFinger = finger;
        _coroutine = StartCoroutine(HandleFinger());
    }

    private void HandleFingerUp()
    {
        _coroutine = null;
        _currentFinger = null;
        Move = 0;
    }

    private IEnumerator HandleFinger()
    {
        Vector2 startFingerPosition = _currentFinger.screenPosition;

        while (_fingerDownTime < _fingerDownDelay)
        {
            yield return null;
            _fingerDownTime += Time.deltaTime;

            if (_currentFinger.isActive == false)
            {
                MoveDownPressed?.Invoke();
                yield return null;
                JumpPressed?.Invoke();

                HandleFingerUp();
                yield break;
            }

            if (Mathf.Abs(_currentFinger.screenPosition.x - startFingerPosition.x) > _fingerMinRejectX)
                break;
        }

        while (_currentFinger.isActive)
        {
            Move = _currentFinger.screenPosition.x - startFingerPosition.x;
            Move = Mathf.Clamp(Move, -_fingerMaxMoveRejectX, _fingerMaxMoveRejectX);

            yield return null;
        }

        HandleFingerUp();
    }
}