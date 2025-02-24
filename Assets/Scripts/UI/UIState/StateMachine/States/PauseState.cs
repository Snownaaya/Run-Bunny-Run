using System;
using UnityEngine;

public class PauseState : MainManuState
{
    public PauseState(ISwitcher switcher, SettingMenu setting) : base(switcher, setting) { }

    public override void Enter()
    {
        base.Enter();
        SettingMenu.PauseButton.onClick.AddListener(OnPauseClick);
    }

    public override void Exit()
    {
        base.Exit();
        SettingMenu.PauseButton.onClick.RemoveListener(OnPauseClick);
    }

    private void OnPauseClick()
    {
        Time.timeScale = 0;
        SettingMenu.CanvasGroup.alpha = 1f;
        SettingMenu.CanvasGroup.interactable = true;
        SettingMenu.CanvasGroup.blocksRaycasts = true;


        if (SettingMenu.ReturnButton)
            StateSwitcher.SwitchState<ReturnState>();
        else if (SettingMenu.RestartButton)
            StateSwitcher.SwitchState<RestartState>();
    }
}