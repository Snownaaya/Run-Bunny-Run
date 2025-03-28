using UnityEngine;
using UnityEngine.Audio;

public class ButtonAudio : MonoBehaviour
{
    protected const string EffectButton = nameof(EffectButton);

    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private AudioSource _audioSource;

    private float _minValueMusic = -80f;
    private float _maxValueMusic = 20f;

    private void SetVolume(float volume)
    {
        float logVolume = Mathf.Log10(volume) * 20;
        logVolume = Mathf.Clamp(logVolume, _minValueMusic, _maxValueMusic);
        _mixer.SetFloat(EffectButton, logVolume);
    }
}
