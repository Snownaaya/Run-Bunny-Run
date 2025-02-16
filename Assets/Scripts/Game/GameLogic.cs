using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private EndScreen _endScreen;
    [SerializeField] private Player _player;
    [SerializeField] private ScoreView scoreView;
    [SerializeField] private LevelStart _levelStart;
    [SerializeField] private RoadSpawner _roadSpawner;
    [SerializeField] private DecorSpawner _decorSpawner;

    [SerializeField] private RoaderStorage _roaderStorage;

    private PlayerWallet _wallet;
    private ScoreCounter _scoreCounter;
    private ScorePresenter _scorePresenter;


    private void Awake()
    {
        _scoreCounter = new ScoreCounter();
        _wallet = new PlayerWallet();

        _scorePresenter = new ScorePresenter(_scoreCounter, scoreView);
    }

    private void Start()
    {
        _levelStart.Open();
        Time.timeScale = 0;

        foreach (Roader road in _roaderStorage.ActiveRoads)
            road.Initialize(_scoreCounter);
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
        Time.timeScale = 1;
    }

    private void OnGameOver()
    {
        _endScreen.Open();
        _roaderStorage.ResetStorage();
        Time.timeScale = 0;
    }

    private void OnResetGame()
    {
        _endScreen.Close();
        Time.timeScale = 1;
        _roadSpawner.ClearPool();
        _decorSpawner.ClearAllDecor();
        int currentIndexScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndexScene);
    }
}