using System;
using UnityEngine;

public class LevelStart : Window
{
    public event Action StartButtonClicked;

    public override void Close()
    {
        CanvasGroup.alpha = 0;
        Button.interactable = false;
        Button.gameObject.SetActive(false);
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1;
        Button.interactable = true;
        Button.gameObject.SetActive(true);
    }

    protected override void OnButtonClick() =>
        StartButtonClicked?.Invoke();
}
