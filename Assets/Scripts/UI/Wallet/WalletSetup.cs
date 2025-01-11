using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerAudio))]
public class WalletSetup : MonoBehaviour
{
    [Inject]
    private CoinSpawner _spawner;

    [SerializeField] private ClampedAmountWithIcon _view;

    private PlayerAudio _playerAudio;
    private WalletPresenter _presenter;
    private PlayerWallet _model;

    private void Awake()
    {
        _model = new PlayerWallet();
        _presenter = new WalletPresenter(_view, _model);
        _playerAudio = GetComponent<PlayerAudio>();
    }

    private void OnEnable() =>
        _presenter.Enable();

    private void OnDisable() =>
        _presenter.Disable();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            _model.AddCoin();
            _spawner.ReturnObject(coin);
            _playerAudio.Play();
        }
    }
}
