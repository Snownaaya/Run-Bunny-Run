using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class TimeHandler
    {
        private static TimeHandler _instance;

        private bool _isPaused;

        public TimeHandler() { }

        public static TimeHandler Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TimeHandler();

                return _instance;
            }
            set => _instance = value;
        }

        public void Pause()
        {
            Time.timeScale = 0;
            _isPaused = true;
        }

        public void Play()
        {
            Time.timeScale = 1;
            _isPaused = false;
        }
    }
}