using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ClampedAmountWithIcon : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        try
        {
            Validate();
        }
        catch(Exception e)
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

    public void SetAmount(int amount) =>
        _text.text = $"{amount}";
}
