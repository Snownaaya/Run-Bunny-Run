using UnityEngine;

public class Notifier : MonoBehaviour
{
    [SerializeField] private GameLogic _gameLogic;

    [SerializeField] private int _duration;

    private int _minDuration = 1;

    private void OnValidate()
    {
        if (_duration < _minDuration)
            _duration = _minDuration;
    }

    private void OnEnable() =>
        Invoke(nameof(Hide), _duration);

    private void Hide()
    {
        gameObject.SetActive(false);
        _gameLogic.OnGameOver();
    }
}
