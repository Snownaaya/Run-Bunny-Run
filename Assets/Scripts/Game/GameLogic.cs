using UnityEngine;
using UnityEngine.WSA;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EndScreen _endScreen;
    [SerializeField] private Player _player;
    [SerializeField] private RoadSpawner _roadSpawner;
    [SerializeField] private ScoreView scoreView;
    [SerializeField] private Roader _road;

    private ScoreCounter _scoreCounter;
    private ScorePresenter _scorePresenter;

    private void Awake()
    {
        _scoreCounter = new ScoreCounter();

        _scorePresenter = new ScorePresenter(_scoreCounter, scoreView);

        _road.Initialize(_scoreCounter);
    }

    private void OnEnable()
    {
        _startScreen.PlayerButtonClicked += OnPlayButtonClicked;
        _endScreen.RestartButtonClicked += OnPlayButtonClicked;
        _player.GameOver += StopGame;
        _scorePresenter.Enable();
    }

    private void OnDisable()
    {
        _startScreen.PlayerButtonClicked -= OnRestartButtonClicked;
        _endScreen.RestartButtonClicked -= OnRestartButtonClicked;
        _player.GameOver -= StopGame;
        _scorePresenter.Disable();
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
        _player.Reset();
        _roadSpawner.Reset();
        _endScreen.Close();
    }
}
