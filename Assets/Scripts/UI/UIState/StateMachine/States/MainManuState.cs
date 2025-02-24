using UnityEngine;
using UnityEngine.UI;

public abstract class MainManuState : IStates
{
    private SettingMenu _setting;

    protected readonly ISwitcher StateSwitcher;

    public MainManuState(ISwitcher switcher, SettingMenu setting)
    {
        StateSwitcher = switcher;
        _setting = setting;
    }

    public SettingMenu SettingMenu => _setting;

    public virtual void Enter()
    {
        Debug.Log(GetType());
    }

    public virtual void Exit() { }
}
