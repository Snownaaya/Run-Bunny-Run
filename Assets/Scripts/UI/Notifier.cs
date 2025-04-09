using UnityEngine;

public class Notifier : MonoBehaviour
{
    [SerializeField] private int _duration;
    [SerializeField] private RevivePanel _panel;

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
        _panel.Open();
    }
}
