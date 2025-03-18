using UnityEngine;

public class TutorialView : MonoBehaviour
{
    private const string PointerVertical = nameof(PointerVertical);
    private const string PointerHorizontal = nameof(PointerHorizontal);

    private Animator _animator;

    public void Init() =>
        _animator = GetComponent<Animator>();

    public void StartPointerVertical() =>
        _animator.SetBool(PointerVertical, true);

    public void StopPointerVertical() =>
        _animator.SetBool(PointerVertical, false);

    public void StartPointerHorizontal() =>
        _animator.SetBool(PointerHorizontal, true);

    public void StopPointerHorizontal() =>
        _animator.SetBool(PointerHorizontal, false);
}