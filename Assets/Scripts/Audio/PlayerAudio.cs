using UnityEngine;

public class PlayerAudio : AudioBase
{
    [SerializeField] private float _delay;
    [SerializeField] private SoundSetting _coinAudio;
    [SerializeField] private SoundSetting _jumpSound;
    [SerializeField] private SoundSetting _footstep;

    private bool _isRunPlaying = true;
    private Animator _animator;

    public void Initialize(Animator animator) =>
        _animator = animator;

    public void PlayJumpSound()
    {
        if(_animator.GetBool(AnimatorData.Parameters.Jump))
            Play(_jumpSound);
    }

    public void Play() =>
        Play(_coinAudio);

    public void PlayFootStep() =>
        Play(_footstep);
}
