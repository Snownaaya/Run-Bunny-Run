using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : ObjectPool<Coin>
{
    private const string Position = nameof(Position);

    [SerializeField] private int _delay;
    [SerializeField] private List<Transform> _points;

    [field: SerializeField] public Coin Coin { get; private set; }

    [Header(Position)]
    [SerializeField] private float _verticalMinBounds;
    [SerializeField] private float _verticalMaxBounds;
    [SerializeField] private float _horizontalMinBounds;
    [SerializeField] private float _horizontalMaxBounds;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        foreach (var pair in _points)
        {
            Coin coin = GetObject(Coin);
            coin.transform.position = pair.position;
            yield return new WaitWhile(() => Coin.MinCoin >= Coin.MaxCoin);
        }
    }

    public void ReturnCoin(Coin coin)
    {
        ReturnObject(coin);
    }
}
