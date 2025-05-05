using Assets.Scripts.Utils;

public class ReturnState : MainManuState
{
    public ReturnState(ISwitcher switcher, SettingMenu setting) : base(switcher, setting) { }

    public override void Enter()
    {
        base.Enter();

        TimeHandler.Instance.Play();
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