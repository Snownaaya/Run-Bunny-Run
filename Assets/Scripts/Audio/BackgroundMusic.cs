using UnityEngine;

namespace Assets.Scripts.Audio
{
    public class BackgroundMusic : MonoBehaviour
    {
        [SerializeField] private AudioSource _backgroundMusic;

        public static BackgroundMusic Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlayMusic()
        {
            if (!_backgroundMusic.isPlaying)
                _backgroundMusic.Play();
        }

        public void StopMusic() =>
            _backgroundMusic.Stop();
    }
}
