using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class ScoreView : MonoBehaviour, IScoreView
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        try
        {
            Validate();
        }
        catch (Exception e)
        {
            enabled = false;
            throw e;
        }
    }

    private void Validate()
    {
        if (_icon == null)
            throw new InvalidOperationException();

        if (_text == null)
            throw new InvalidOperationException();
    }

    public void UpdateScore(int amount) =>
        _text.text = $"Score: {amount}";
}