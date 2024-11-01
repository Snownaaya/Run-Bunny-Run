using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EndScreen _endScreen;
    [SerializeField] private Player _player;
    [SerializeField] private RoadSpawner _roadSpawner;

    private ScoreCounter _scoreCounter;
    private ScorePresenter _scorePresenter;

    private void Awake()
    {
        _scoreCounter = new ScoreCounter();
        _scorePresenter = new ScorePresenter(_scoreCounter, _scoreView);

        if (_scoreCounter == null && _scoreView == null && _scorePresenter == null)
            print($"{_scoreCounter} == score null, {_scoreView} == view null, {_scorePresenter} == presenter null");
    }

    private void OnEnable()
    {
        if (_scorePresenter == null)
        {
            _scorePresenter.Enable();
            print("is null");
        }
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
        _scoreCounter.Reset();
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        _player.Reset();
        _scoreCounter.Reset();
        _roadSpawner.Reset();
        _endScreen.Close();
    }
}
