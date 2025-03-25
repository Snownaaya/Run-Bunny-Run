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
}