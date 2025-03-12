using UnityEngine;

public class PlayerAudio : AudioBase
{
    [SerializeField] private float _delay;
    [SerializeField] private SoundSetting _coinAudio;
    [SerializeField] private SoundSetting _jumpSound;
    [SerializeField] private SoundSetting _footstep;

    public void PlayJumpSound() =>
        Play(_jumpSound);

    public void Play() =>
        Play(_coinAudio);

    public void PlayFootStep() =>
        Play(_footstep);
}
