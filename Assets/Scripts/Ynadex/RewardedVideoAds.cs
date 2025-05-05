using UnityEngine;
using YG;
using System;

public class RewardedVideoAds : MonoBehaviour
{
    [SerializeField] private Notifier _notifier;

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

    public void ShowRewardedAd() =>
        YandexGame.RewVideoShow(_advertisingId);

    private void OnErrorVideoEvent() =>
        _notifier.gameObject.SetActive(true);

    private void OnRewarded(int id)
    {
        if (id == _advertisingId)
            OnReviveGranted?.Invoke();
    }
}
