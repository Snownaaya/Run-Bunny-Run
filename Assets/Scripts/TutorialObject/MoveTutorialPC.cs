using DG.Tweening;
using UnityEngine;

public class MoveTutorialPC : TutorialStepCondiction, ITutorialObjectEventSource
{
    [SerializeField] private RectTransform[] _rectTransforms;

    private Sequence _animation;

    public override void Disable()
    {
        gameObject.SetActive(false);
        Completed = true;
    }

    public override void Enable() =>
        gameObject.SetActive(true);

    private void ShowAnimation()
    {
        StopAnimation();

        _animation = DOTween.Sequence();

        foreach (RectTransform rectTransform in _rectTransforms)
        {
            rectTransform.localScale = Vector3.zero;
            _animation.Append(rectTransform.DOScale(1f, 0.5f).From(0f).SetEase(Ease.OutBounce))
                .SetUpdate(true);
            _animation.AppendInterval(0.2f);
        }
    }

    private void HideAnimation()
    {
        StopAnimation();
        
        _animation = DOTween.Sequence();

        foreach (RectTransform rectTransform in _rectTransforms)
        {
            rectTransform.localScale = Vector3.zero;
            _animation.Append(rectTransform.DOScale(1f, 0.5f).SetEase(Ease.OutBounce))
                .SetUpdate(true);
            _animation.AppendInterval(0.2f);
        }
    }

    private void StopAnimation()
    {
        if (_animation != null)
        {
            _animation.Kill();
            _animation = null;
        }
    }
}