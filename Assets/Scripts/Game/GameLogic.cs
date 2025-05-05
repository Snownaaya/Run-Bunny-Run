using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private EndScreen _endScreen;
    [SerializeField] private Character _player;
    [SerializeField] private RoadSpawner _roadSpawner;
    [SerializeField] private DecorSpawner _decorSpawner;
    [SerializeField] private LandingSpawner _landingSpawner;
    [SerializeField] private RewardedVideoAds _rewardedAds;
    [SerializeField] private CoinParticleSpawner _coinParticleSpawner;
    [SerializeField] private RevivePanel _revivePanel;
    [SerializeField] private HandleRoadSpeed _handleRoadSpeed;
    [SerializeField] private WalletSetup _setup;
    [SerializeField] private RoaderStorage _roaderStorage;
    [SerializeField] private ScoreCounter _scoreCounter;

    private PlayerWallet _wallet;
    private ScorePresenter _scorePresenter;

    private void Awake()
    {
        _wallet = _setup.Wallet;
        _scorePresenter = new ScorePresenter(_scoreCounter, _scoreView);
        _revivePanel.Initialize(_player);
        //_roadSpawner.Initialize(_scoreCounter);
    }

    private void OnEnable()
    {
        _scorePresenter.Enable();
        _endScreen.RestartButtonClicked += OnResetGame;
        _player.GameOver += OnGameOver;
        _revivePanel.OnReviveWithAdRequested += OnReviveWithAdRequested;
        _revivePanel.OnReviveWithCoinsRequested += OnReviveWithCoinsRequested;
        _rewardedAds.OnReviveGranted += OnReviveGranted;
    }

    private void OnDisable()
    {
        _scorePresenter.Disable();
        _endScreen.RestartButtonClicked -= OnResetGame;
        _player.GameOver -= OnGameOver;
        _revivePanel.OnReviveWithAdRequested -= OnReviveWithAdRequested;
        _revivePanel.OnReviveWithCoinsRequested -= OnReviveWithCoinsRequested;
        _rewardedAds.OnReviveGranted -= OnReviveGranted;
    }

    public void OnGameOver()
    {
        _endScreen.Open();
        _revivePanel.Open();
        TimeHandler.Instance.Pause();
    }

    private void OnReviveWithAdRequested() =>
        _rewardedAds.ShowRewardedAd();

    private void OnReviveWithCoinsRequested(int coinCost)
    {
        if (_wallet.SpendCoins(coinCost))
            RevivePlayer();
    }

    private void OnReviveGranted() =>
        RevivePlayer();

    private void RevivePlayer()
    {
        _revivePanel.Close();
        TimeHandler.Instance.Play();
        _player.Revive();
    }

    private void OnResetGame()
    {
        _endScreen.Close();
        TimeHandler.Instance.Play();
        _roadSpawner.ClearPool();
        _scoreCounter.Reset();
        _wallet.Reset();
        _player.Reset();
        _roaderStorage.ResetStorage();
        _landingSpawner.ClearPool();
        _coinParticleSpawner.ClearPool();
        _decorSpawner.ClearAllDecor();
        int currentIndexScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndexScene);
    }
}