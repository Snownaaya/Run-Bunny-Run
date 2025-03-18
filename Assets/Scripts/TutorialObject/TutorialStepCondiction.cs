using UnityEngine;

public abstract class TutorialStepCondiction : MonoBehaviour, ITutorialObjectEventSource, ITutorialStepCondiction
{
    private bool _isComplete = false;

    public bool Completed
    {
        get => _isComplete;
        set => _isComplete = value;
    }

    public abstract void Disable();
    public abstract void Enable();
}
