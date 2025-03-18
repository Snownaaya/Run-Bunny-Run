using System.Collections;
using UnityEngine;

public class TutorialRoot : MonoBehaviour
{
    private const string MoveTutorialKey = nameof(MoveTutorialKey);

    [SerializeField] private MoveTutorialMobile _mobileTutorialView;
    [SerializeField] private MoveTutorialPC _pcTutorialView;

    private IInputProvider _inputProvider;
    private ITutorialObjectEventSource _currentTutorialView;
    private TutorialStepCondiction _stepCondiction;

    public bool Complete => PlayerPrefs.HasKey(MoveTutorialKey);

    private void Start()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            _currentTutorialView = _pcTutorialView;
            _stepCondiction = _pcTutorialView;
            _pcTutorialView.Enable();
            _mobileTutorialView.Disable();
    }
        else if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            _currentTutorialView = _mobileTutorialView;
            _stepCondiction = _mobileTutorialView;
            _mobileTutorialView.Enable();
            _pcTutorialView.Disable();
        }

        StartCoroutine(StartWithDelay());
    }

    private IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(5f);

        if (_stepCondiction.Completed == false)
        {
            PlayerPrefs.SetInt(MoveTutorialKey, 1);
            _stepCondiction.Completed = true;
            _currentTutorialView.Disable();
        }
    }
}
