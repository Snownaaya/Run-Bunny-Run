using UnityEngine;
using UnityEngine.Audio;

public class ButtonAudio : MonoBehaviour
{
    protected const string EffectButton = nameof(EffectButton);

    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private AudioSource _audioSource;
}