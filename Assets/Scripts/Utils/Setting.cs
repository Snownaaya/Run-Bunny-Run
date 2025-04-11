using System;
using UnityEngine;

public abstract class Setting : ISetting
{
    private readonly string _saveKey;

    protected Setting(string saveKey) =>
        _saveKey = saveKey;

    public bool IsEnable =>
        Convert.ToBoolean(PlayerPrefs.GetInt(_saveKey, 1));

    public void Disable() =>
        SetActive(false);

    public void Enable() =>
        SetActive(true);

    private void SetActive(bool value) => 
        PlayerPrefs.SetInt(_saveKey, Convert.ToInt32(value));
}