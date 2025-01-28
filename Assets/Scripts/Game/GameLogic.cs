using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private EndScreen _endScreen;
    [SerializeField] private Player _player;
    [SerializeField] private ScoreView scoreView;
    [SerializeField] private LevelStart _levelStart;

    [SerializeField] private RoaderStorage _roaderStorage;

    private PlayerWallet _wallet;
    private ScoreCounter _scoreCounter;
    private ScorePresenter _scorePresenter;

    private HandleRoadMovement _handleRoadMovement;

    private void Awake()
    {
        _scoreCounter = new ScoreCounter();
        _wallet = new PlayerWallet();

        _scorePresenter = new ScorePresenter(_scoreCounter, scoreView);

        _handleRoadMovement = new HandleRoadMovement(_roaderStorage, this);
    }

    private void Start()
    {
        _levelStart.Open();
        Time.timeScale = 0;

        foreach (Roader road in _roaderStorage.ActiveRoads)
            road.Initialize(_scoreCounter);

        _handleRoadMovement.StartIncreaseSpeed();
    }

    private void OnEnable()
    {
        _scorePresenter.Enable();
        _levelStart.StartButtonClicked += OnStartGame;
        _endScreen.RestartButtonClicked += OnResetGame;
        _player.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _scorePresenter.Disable();
        _levelStart.StartButtonClicked -= OnStartGame;
        _endScreen.RestartButtonClicked -= OnResetGame;
        _player.GameOver -= OnGameOver;
    }

    private void OnStartGame()
    {
        _levelStart.Close();
        _handleRoadMovement.ResetSpeed(100f);
        Time.timeScale = 1;
    }

    private void OnGameOver()
    {
        _endScreen.Open();
        Time.timeScale = 0;
    }

    private void OnResetGame()
    {
        _endScreen.Close();
        Time.timeScale = 1;
        _handleRoadMovement.ResetSpeed(100f);
        int currentIndexScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndexScene);
    }
}