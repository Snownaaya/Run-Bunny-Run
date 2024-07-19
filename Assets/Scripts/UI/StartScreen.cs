using UnityEngine;
using System;

public class StartScreen : Window
{
    public event Action PlayerButtonClicked;

    public override void Open()
    {
        CanvasGroup.alpha = 1;
        Button.interactable = true;
    }

    public override void Close()
    {
        CanvasGroup.alpha = 0;
        Button.interactable = false;
    }

    protected override void OnButtonClick() =>
        PlayerButtonClicked?.Invoke();
}
