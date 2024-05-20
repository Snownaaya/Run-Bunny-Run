using System.Collections.Generic;
using UnityEngine;

public abstract class PoolCoin<T> : MonoBehaviour, IResetteble where T : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private Transform _cointainer;

    private Queue<Coin> _poolCoin = new Queue<Coin>();

    public Coin GetCoin()
    {
        if (_poolCoin.Count == 0)
        {
            Coin coin = Instantiate(_coin, _cointainer);
            return coin;
        }

        Coin poolCoin = _poolCoin.Dequeue();
        return poolCoin;
    }

    public void ReturnCoin(Coin coin)
    {
        coin.gameObject.SetActive(false);
        _poolCoin.Enqueue(coin);
    }
}
