using DG.Tweening;
using UnityEngine;

public abstract class MainManuState : IStates
{
    private SettingMenu _setting;
    private Sequence _animation;

    protected readonly ISwitcher StateSwitcher;

    public MainManuState(ISwitcher switcher, SettingMenu setting)
    {
        StateSwitcher = switcher;
        _setting = setting;
    }

    public SettingMenu SettingMenu => _setting;

    public void Show()
    {
        KillCurrentAnimationIfActive();

        _animation = DOTween.Sequence();

        _animation
            .Append(SettingMenu.CanvasGroup.transform.DOScale(1.337628f, 0.5f).From(0f).SetEase(Ease.Linear))
            .Join(SettingMenu.RectTransform.DOAnchorPos(SettingMenu.TargetBody, 1f).From(SettingMenu.CurrentPosition))
            .Append(SettingMenu.RestartButton.transform.DOScale(1f, 1f).From(0f).SetEase(Ease.OutCirc))
            .Append(SettingMenu.ReturnButton.transform.DOScale(1f, 1f).From(0f).SetEase(Ease.OutCirc))
            .SetUpdate(true)
            .Restart();
    }

    public void Hide()
    {
        KillCurrentAnimationIfActive();

        _animation = DOTween.Sequence();

        _animation
            .Append(SettingMenu.CanvasGroup.DOFade(1f, 0f).From(1))
            .Join(SettingMenu.RectTransform.DOAnchorPos(SettingMenu.CurrentPosition, 1f).From(SettingMenu.TargetBody))
            .Restart();
    }


    private void KillCurrentAnimationIfActive()
    {
        if (InAnimation())
            _animation.Kill();
    }

    private bool InAnimation() =>
    _animation != null && _animation.active;

    public virtual void Enter() =>
        Debug.Log(GetType());

    public virtual void Exit() { }
}