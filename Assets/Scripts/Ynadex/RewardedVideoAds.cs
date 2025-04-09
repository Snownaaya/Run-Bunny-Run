using UnityEngine;
using YG;
using System;

public class RewardedVideoAds : MonoBehaviour
{
    [SerializeField] private Notifier _notifier;
    [SerializeField] private SettingMenu _settingMenu;

    private int _advertisingId = 0;

    public event Action OnReviveGranted;

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += OnRewarded;
        YandexGame.ErrorVideoEvent += OnErrorVideoEvent;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= OnRewarded;
        YandexGame.ErrorVideoEvent -= OnErrorVideoEvent;
    }

    public void ShowRewardedAd()
    {
        _settingMenu.PauseButton.interactable = false;
        YandexGame.RewVideoShow(_advertisingId);
    }

    private void OnErrorVideoEvent()
    {
        _settingMenu.PauseButton.interactable = true;
        _notifier.gameObject.SetActive(true);
    }

    private void OnRewarded(int id)
    {
        _settingMenu.PauseButton.interactable = true;

        if (id == _advertisingId)
            OnReviveGranted?.Invoke();
    }
}
