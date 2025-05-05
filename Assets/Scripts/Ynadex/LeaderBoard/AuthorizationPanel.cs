using UnityEngine;
using UnityEngine.UI;
using YG;

public class AuthorizationPanel : MonoBehaviour
{
    [SerializeField] private Button _authoriztion;
    [SerializeField] private Button _close;

    private void OnEnable()
    {
        _authoriztion.onClick.AddListener(OnClickButton);
        _close.onClick.AddListener(OnClose);
    }

    private void OnDisable()
    {
        _authoriztion.onClick.RemoveListener(OnClickButton);
        _close.onClick.RemoveListener(OnClose);
    }

    private void OnClickButton()
    {
        YandexGame.AuthDialog();
        OnClose();
    }

    private void OnClose() =>
        gameObject.SetActive(false);
}
