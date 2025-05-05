using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private Button _closeButton;

    private void OnEnable() =>
        _closeButton.onClick.AddListener(OnCloseButton);

    private void OnDisable() =>
        _closeButton.onClick.RemoveListener(OnCloseButton);

    private void OnCloseButton() =>
        gameObject.SetActive(false);
}