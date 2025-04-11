using System;
using UnityEngine;
using UnityEngine.UI;

public class RevivePanel : Window
{
    [SerializeField] private Button _reviveWithCoinsButton;

    [SerializeField] private int _coinCost;

    private Character _player;

    public event Action OnReviveWithAdRequested;
    public event Action<int> OnReviveWithCoinsRequested;

    public void Initialize(Character character) =>
        _player = character;

    private new void OnEnable()
    {
        base.OnEnable();
        Button.onClick.AddListener(OnReviveWithAdClick);
        _reviveWithCoinsButton.onClick.AddListener(OnReviveWithCoinsClick);
    }


    private new void OnDisable()
    {
        base.OnDisable();
        Button.onClick.RemoveListener(OnReviveWithAdClick);
        _reviveWithCoinsButton.onClick.RemoveListener(OnReviveWithCoinsClick);
    }

    public override void Close()
    {
        CanvasGroup.alpha = 0f;
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.gameObject.SetActive(false);
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1f;
        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.gameObject.SetActive(true);
    }

    protected override void OnButtonClick() =>
        Close();

    private void OnReviveWithAdClick() =>
        OnReviveWithAdRequested.Invoke();

    private void OnReviveWithCoinsClick() =>
        OnReviveWithCoinsRequested.Invoke(_coinCost);
}
