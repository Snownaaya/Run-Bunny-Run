using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerAudio))]
public class WalletSetup : MonoBehaviour
{
    [Inject] private CoinSpawner[] _spawner;

    [SerializeField] private ClampedAmountWithIcon _view;
    [SerializeField] private CoinParticle _coinParticle;
    [SerializeField] private PooledParticle _pooledParticle;

    private PlayerAudio _playerAudio;
    private WalletPresenter _presenter;
    private PlayerWallet _model;

    private float _delay = 1f;

    private void Awake()
    {
        _model = new PlayerWallet();
        _presenter = new WalletPresenter(_view, _model);
        _playerAudio = GetComponent<PlayerAudio>();

        _coinParticle.Initialize(_pooledParticle, this);
    }

    private void OnEnable() =>
        _presenter.Enable();

    private void OnDisable() =>
        _presenter.Disable();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            PooledParticle pooledParticle = _coinParticle.Spawn();
            _playerAudio.Play();
            _model.AddCoin();
            _spawner[0].ReturnCoin(coin);
            StartCoroutine(ReturnParticle(pooledParticle));
        }
    }

    private IEnumerator ReturnParticle(PooledParticle pooledParticle)
    {
        yield return new WaitForSeconds(_delay);
        _coinParticle.ReturnCoinParticle(pooledParticle);
    }
}
