using System.Collections;
using System.Drawing;
using UnityEngine;

public class CoinSpawner : ObjectPool<Coin>
{
    private const string Position = nameof(Position);

    [SerializeField] private int _delay;
    [SerializeField] private Transform[] _points;

    [field: SerializeField] public Coin Coin { get; private set; }

    private int _count = 5;

    private void Start()
    {
        for (int i = 0; i < _count; i++)
            StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        var wait = new WaitForSeconds(_delay);
        Generate();

        yield return new WaitWhile(() => Coin.MinCoin >= Coin.MaxCoin);
        yield return wait;
    }

    private void Generate()
    {
        int randomZposition = Random.Range(0, _points.Length);

        Coin coin = GetObject(Coin);
        coin.transform.position = _points[randomZposition].position;
    }

    public void ReturnCoin(Coin coin) =>
        ReturnObject(coin);
}
