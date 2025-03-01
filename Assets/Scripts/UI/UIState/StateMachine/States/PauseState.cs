using UnityEngine;

public class PauseState : MainManuState
{
    public PauseState(ISwitcher switcher, SettingMenu setting) : base(switcher, setting) { }

    public override void Enter()
    {
        base.Enter();

        Time.timeScale = 0;
        SettingMenu.CanvasGroup.alpha = 1f;
        SettingMenu.CanvasGroup.interactable = true;
        SettingMenu.CanvasGroup.blocksRaycasts = true;

        Show();
    }

    public override void Exit()
    {
        base.Exit();
    }
}