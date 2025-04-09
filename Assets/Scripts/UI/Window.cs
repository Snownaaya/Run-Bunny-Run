using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour
{
    [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }
    [field: SerializeField] public Button Button { get; private set; }

    protected void OnEnable() =>
        Button.onClick.AddListener(OnButtonClick);

    protected void OnDisable() =>
        Button.onClick.RemoveListener(OnButtonClick);

    protected abstract void OnButtonClick();

    public abstract void Open();

    public abstract void Close();

    public virtual void Reset()
    {
        Close();
        Button.interactable = true;
    }
}