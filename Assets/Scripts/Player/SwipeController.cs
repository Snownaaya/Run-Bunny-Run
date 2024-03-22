using UnityEngine;

public abstract class SwipeController : MonoBehaviour
{
    public const string Horizontal = nameof(Horizontal);
    public static bool IsTapDetected, IsSwipeRightDetected, IsSwipeLeftDetected, IsSwipeUpDetected, IsSwipeDownDetected;

    private Vector2 _swipeDelta, _startTouch;
    private bool _isDraging = true;

    private void Update()
    {
        SwipeComputerController();
        SwipePhoneController();
    }

    private void SwipeComputerController()
    {
        if (Input.GetMouseButton(0))
        {
            IsTapDetected = true;
            _isDraging = true;
            _startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            _isDraging = false;
            Reset();
        }
    }

    private void SwipePhoneController()
    {
        if (_isDraging)
        {

        }
    }

    private void Reset()
    {
        _swipeDelta = _startTouch = Vector2.zero;
        _isDraging = false;
    }
}
