using UnityEngine;

public class CoinSpawner : ObjectPool<Coin>
{
    [SerializeField] private float _delay;
    [SerializeField] private Coin _coin;

    [SerializeField] private float _horizontalMinBounds;
    [SerializeField] private float _horizontalMaxBounds;

    [SerializeField] private float _verticalMinBounds;
    [SerializeField] private float _verticalMaxBounds;

    public void Spawn(Coin coin, Transform parent)
    {
        Coin coins = GetObject(coin);
        coins.gameObject.SetActive(true);

        coins.transform.SetParent(parent);
        float positionZ = parent.position.z;

        Vector3 worldPosition = RandomCoinPosition(positionZ);
        coins.transform.position = worldPosition;
    }

    private Vector3 RandomCoinPosition(float positionZ)
    {
        positionZ = RandomGenerator.Range(_horizontalMinBounds, _horizontalMaxBounds);
        float positionY = RandomGenerator.Range(_verticalMinBounds, _verticalMaxBounds);

        Vector3 worldPosition = new Vector3(0, positionY, positionZ);

        return worldPosition;
    }
}
