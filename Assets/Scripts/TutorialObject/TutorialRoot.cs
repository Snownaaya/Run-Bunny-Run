using System.Collections;
using UnityEngine;
using YG;

public class TutorialRoot : MonoBehaviour, ITutorialStepCondiction
{
    private const string MoveTutorialKey = nameof(MoveTutorialKey);

    [SerializeField] private MoveTutorialMobile _mobileTutorialView;
    [SerializeField] private MoveTutorialPC _pcTutorialView;

    private ITutorialObjectEventSource _currentTutorialView;

    private TutorialStepCondiction _stepCondiction;

    private float _delay = 5f;

    public bool Completed => PlayerPrefs.HasKey(MoveTutorialKey);

    private void Start()
    {
        if (_pcTutorialView == null || _mobileTutorialView == null)
            return;

        if (Completed)
            return;

        if (YandexGame.EnvironmentData.isDesktop)
        {
            _currentTutorialView = _pcTutorialView;
            _stepCondiction = _pcTutorialView;
            _pcTutorialView.Enable();
        }
        else if (YandexGame.EnvironmentData.isMobile)
        {
            _currentTutorialView = _mobileTutorialView;
            _stepCondiction = _mobileTutorialView;
            _mobileTutorialView.Enable();
        }

        StartCoroutine(StartWithDelay());
    }

    private IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(_delay);

        PlayerPrefs.SetInt(MoveTutorialKey, 1);
        _currentTutorialView.Disable();
        PlayerPrefs.Save();
    }
}
