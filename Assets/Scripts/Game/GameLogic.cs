using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EndScreen _endScreen;
    [SerializeField] private Player _player;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private Roader _roader;
    [SerializeField] private RoadSpawner _roadSpawner;

    private void OnEnable()
    {
        _startScreen.PlayerButtonClicked += OnPlayButtonClicked;
        _endScreen.RestartButtonClicked += OnPlayButtonClicked;
        _player.GameOver += StopGame;
    }

    private void OnDisable()
    {
        _startScreen.PlayerButtonClicked -= OnRestartButtonClicked;
        _endScreen.RestartButtonClicked -= OnRestartButtonClicked;
        _player.GameOver -= StopGame;
    }

    private void Start()
    {
        Time.timeScale = 0;
        _startScreen.Open();
    }

    private void OnPlayButtonClicked()
    {
        StartGame();
        _startScreen.Close();
    }

    private void OnRestartButtonClicked() =>
        StartGame();
    
    private void StopGame()
    {
        Time.timeScale = 0;
        _endScreen.Open();
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        _roader.Reset();
        _player.Reset();
        _scoreCounter.Reset();
        _roadSpawner.Reset();
        _endScreen.Close();
    }
}
