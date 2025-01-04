using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private EndScreen _endScreen;
    [SerializeField] private Player _player;
    [SerializeField] private ScoreView scoreView;

    [SerializeField] private Roader[] _road;

    private PlayerWallet _wallet;
    private ScoreCounter _scoreCounter;
    private ScorePresenter _scorePresenter;

    private void Awake()
    {
        _scoreCounter = new ScoreCounter();
        _wallet = new PlayerWallet();

        _scorePresenter = new ScorePresenter(_scoreCounter, scoreView);

        foreach (Roader road in _road)
            road.Initialize(_scoreCounter);
    }

    private void Start()
    {
        PauseGame();
    }

    private void OnEnable()
    {
        _player.GameOver += OnGameOver;
        _scorePresenter.Enable();
        _endScreen.RestartButtonClicked += OnRestartButtonClicked;
    }

    private void OnDisable()
    {
        _player.GameOver -= OnGameOver;
        _scorePresenter.Disable();
        _endScreen.RestartButtonClicked -= OnRestartButtonClicked;
    }

    private void OnStartGame()
    {
        ResumeGame();
    }

    private void OnRestartButtonClicked()
    {
        ResetGame();
    }

    private void OnGameOver()
    {
        _endScreen.Open();
        PauseGame();
    }

    private void ResetGame()
    {
        _endScreen.Close();
        Time.timeScale = 1;
        int currentIndexScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndexScene);
    }

    private void PauseGame() =>
        Time.timeScale = 0;

    private void ResumeGame() =>
        Time.timeScale = 1;
}
