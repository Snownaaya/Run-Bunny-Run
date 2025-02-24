using Assets.Scripts.Audio;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class SettingMenu : MonoBehaviour
{
    [SerializeField] private ScreenFader _screenFader;

    [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }
    [field: SerializeField] public Button PauseButton { get; private set; }
    [field: SerializeField] public Button ReturnButton { get; private set; }
    [field: SerializeField] public Button RestartButton { get; private set; }

    private SettingState _state;

    public ScreenFader ScreenFader => _screenFader;

    private void Awake() =>
        _state = new SettingState(this);
}
