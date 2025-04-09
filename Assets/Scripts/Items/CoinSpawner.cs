using System.Collections;
using UnityEngine;

public class CoinSpawner : ObjectPool<Coin>
{
    private const string Position = nameof(Position);

    [SerializeField] private int _delay;
    [SerializeField] private Transform[] _points;

    [field: SerializeField] public Coin Coin { get; private set; }

    private int _count = 20;

    private void Start() =>
        StartCoroutine(Spawn());

    public IEnumerator Spawn()
    {
        var wait = new WaitForSeconds(_delay);

        while (true)
        {
            for (int i = 0; i < _count; i++)
                Generate();

            yield return wait;
        }

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
