using System.Collections;
using UnityEngine;

public class MoveTutorialMobile : TutorialStepCondiction, ITutorialObjectEventSource
{
    [SerializeField] private TutorialView _view;

    private float _delay = 3f;

    private void Awake() =>
        _view.Init();

    public override void Enable()
    {
        gameObject.SetActive(true);
        StartCoroutine(PlayAnimation());
    }

    public override void Disable()
    {
        if (Completed)
            gameObject.SetActive(false);
    }

    private IEnumerator PlayAnimation()
    {
        _view.StartPointerVertical();
        yield return new WaitForSeconds(_delay);
        _view.StopPointerVertical();

        _view.StartPointerHorizontal();
        yield return new WaitForSeconds(_delay);
        _view.StopPointerHorizontal();

        Completed = true;
    }
}