using Assets.Scripts.Utils;

public class RestartState : MainManuState
{
    private const string StartScene = nameof(StartScene);

    private ICoroutineRunner _runner;

    public RestartState(ISwitcher switcher, SettingMenu setting, ICoroutineRunner runner) : base(switcher, setting)
    {
        _runner = runner;
    }

    public override void Enter()
    {
        base.Enter();

        TimeHandler.Instance.Play();
        _runner.StartCoroutine(SettingMenu.ScreenFader.TransitionCoroutine(StartScene));

        Hide();
    }

    public override void Exit()
    {
        base.Exit();
    }
}