using UnityEngine;

public class ReturnState : MainManuState
{
    public ReturnState(ISwitcher switcher, SettingMenu setting) : base(switcher, setting) { }

    public override void Enter()
    {
        base.Enter();

        Time.timeScale = 1;
        SettingMenu.CanvasGroup.alpha = 0f;
        SettingMenu.CanvasGroup.interactable = false;
        SettingMenu.CanvasGroup.blocksRaycasts = false;

        Hide();
    }

    public override void Exit()
    {
        base.Exit();
    }
}