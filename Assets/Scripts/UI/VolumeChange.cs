using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace Assets.Scripts.Audio
{
    public class VolumeChange : MonoBehaviour
    {
        [SerializeField] private string[] _volume;
        [SerializeField] private Slider _volumeAudio;
        [SerializeField] private AudioMixer _mixer;

        private float _minValueMusic = -80f;
        private float _maxValueMusic = 20f;

        private void OnEnable() =>
            _volumeAudio.onValueChanged.AddListener(SetVolume);

        private void OnDisable() =>
            _volumeAudio.onValueChanged.RemoveListener(SetVolume);

        private void SetVolume(float volume)
        {
            float logVolume = Mathf.Log10(volume) * 20;
            logVolume = Mathf.Clamp(logVolume, _minValueMusic, _maxValueMusic);

            foreach (var volumeExcposed in _volume)
                _mixer.SetFloat(volumeExcposed, logVolume);
        }
    }
}
