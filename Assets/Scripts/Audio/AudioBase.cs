using System;
using UnityEngine;
using UnityEngine.Audio;

public abstract class AudioBase : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private Audio _audio;

    private void Awake() =>
        _audio = new Audio();

    protected void Play(SoundSetting audioSettings)
    {
        if (_audio.IsEnable == false)
            return;

        _audioSource.outputAudioMixerGroup = audioSettings.Group;
        _audioSource.PlayOneShot(audioSettings.Clip);
    }
}

[Serializable]
public class SoundSetting
{
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioMixerGroup _group;

    public AudioClip Clip => _clip;
    public AudioMixerGroup Group => _group;
}
