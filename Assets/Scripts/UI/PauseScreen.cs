using UnityEngine.UI;
using UnityEngine;
using System;

public class PauseScreen : MonoBehaviour
{
    [field: SerializeField] public Button Button { get; private set; }
    [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }

    private void OnEnable() =>
        Button.onClick.AddListener(OnPauseClick);

    private void OnDisable() =>
        Button.onClick.RemoveListener(OnPauseClick);

    private void OnPauseClick()
    {
        try
        {
            if (Time.timeScale == 1)
                PauseGame();
            else
                ResumeGame();
        }
        catch
        {
            Validate();
        }
    }

    private void PauseGame() =>
        Time.timeScale = 0;

    private void ResumeGame() =>
        Time.timeScale = 1;

    private void Validate()
    {
        if (Button == null)
            throw new InvalidOperationException();

        if (CanvasGroup == null)
            throw new InvalidOperationException();
    }
}
