using UnityEngine;
using YG;

public class ShowOnStart : MonoBehaviour
{
    private bool adShown = false;

    void OnEnable() =>
        YandexGame.GetDataEvent += OnSDKReady;

    void OnDisable() =>
        YandexGame.GetDataEvent -= OnSDKReady;

    public void OnSDKReady()
    {
        if (!adShown && YandexGame.SDKEnabled)
        {
            Debug.Log("SDK инициализирован, показываем рекламу!");
            YandexGame.FullscreenShow();
            adShown = true;
        }
    }
}