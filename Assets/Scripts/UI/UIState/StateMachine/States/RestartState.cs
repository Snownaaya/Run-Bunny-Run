public class RestartState : MainManuState
{
    private const string SceneNameToRestart = "StartScene";

    public RestartState(ISwitcher switcher, SettingMenu setting) : base(switcher, setting) { }

    public override void Enter()
    {
        base.Enter();
        SettingMenu.RestartButton.onClick.AddListener(OnRestartClicked);
    }

    public override void Exit()
    {
        base.Exit();
        SettingMenu.RestartButton.onClick.RemoveListener(OnRestartClicked);
    }

    private void OnRestartClicked()
    {
        StateSwitcher.SwitchState<PauseState>();
        SettingMenu.ScreenFader.TransitionToScene("StartScene");
    }
}