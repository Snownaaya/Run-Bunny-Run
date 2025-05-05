using UnityEngine;

public class CoinSpawner : ObjectPool<Coin>
{
    private const string Position = nameof(Position);

    [field: SerializeField] public Coin Coin { get; private set; }

    private int _count = 15;

    public void Generate(Roader roader, Transform[] coinPoints)
    {
        for (int i = 0; i < _count; i++)
        {
            int randomZposition = Random.Range(0, coinPoints.Length);
            Coin coin = GetObject(Coin);
            coin.transform.SetParent(roader.transform);
            coin.transform.position = roader.CoinSpawnPoints[randomZposition].position;
        }
    }

    public void ReturnCoin(Coin coin) =>
        ReturnObject(coin);
}
