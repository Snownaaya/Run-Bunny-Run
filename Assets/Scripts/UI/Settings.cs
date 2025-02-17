using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : Window
{
    public override void Close()
    {
        CanvasGroup.alpha = 0f;
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1f;
        //Button
    }

    protected override void OnButtonClick()
    {
        throw new System.NotImplementedException();
    }
}
