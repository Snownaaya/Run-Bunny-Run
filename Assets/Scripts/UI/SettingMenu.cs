using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private ScreenFader _screenFader;
    [SerializeField] private CoroutineRunner _coroutineRunner;

    [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }
    [field: SerializeField] public RectTransform RectTransform { get; private set; }
    [field: SerializeField] public Button PauseButton { get; private set; }
    [field: SerializeField] public Button ReturnButton { get; private set; }
    [field: SerializeField] public Button RestartButton { get; private set; }

    private SettingState _state;
    private Vector2 _targetBody;
    private Vector2 _currentPosition;

    public ScreenFader ScreenFader => _screenFader;
    public Vector2 CurrentPosition => _currentPosition;
    public Vector2 TargetBody => _targetBody;

    private void Awake()
    {
        _state = new SettingState(this, _coroutineRunner);
        _targetBody = RectTransform.anchoredPosition;
        _currentPosition = new Vector2(_targetBody.x, Screen.height * 2);
    }

    private void OnEnable()
    {
        PauseButton.onClick.AddListener(OnPauseButtonClick);
        RestartButton.onClick.AddListener(OnRestartButtonClick);
        ReturnButton.onClick.AddListener(OnReturnButtonClick);
    }

    private void OnDisable()
    {
        PauseButton.onClick.RemoveListener(OnPauseButtonClick);
        RestartButton.onClick.RemoveListener(OnRestartButtonClick);
        ReturnButton.onClick.RemoveListener(OnReturnButtonClick);
    }

    private void OnPauseButtonClick() =>
        _state.SwitchState<PauseState>();

    private void OnRestartButtonClick() =>
        _state.SwitchState<RestartState>();

    private void OnReturnButtonClick() =>
        _state.SwitchState<ReturnState>();
}
