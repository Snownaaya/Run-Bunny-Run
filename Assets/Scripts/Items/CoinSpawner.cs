using System.Collections;
using UnityEngine;

public class CoinSpawner : ObjectPool<Coin>
{
    [SerializeField] private float _delay;

    [SerializeField] private float _horizontalMinBounds;
    [SerializeField] private float _horizontalMaxBounds;
    [SerializeField] private float _verticalMinBounds;
    [SerializeField] private float _verticalMaxBounds;

    private Coroutine _coroutine;

    private void Start() => _coroutine = StartCoroutine(CoinGenerator());

    private IEnumerator CoinGenerator()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            Spawn();
            yield return wait;
        }
    }

    private void Spawn()
    {
        Coin coin = GetObject();
        coin.transform.position = RandomCoinPosition();
        coin.gameObject.SetActive(true);
    }

    private Vector3 RandomCoinPosition()
    {
        float positionX = RandomGenerator.Range(_horizontalMinBounds, _horizontalMaxBounds);
        float positionZ = RandomGenerator.Range(_verticalMinBounds, _verticalMaxBounds);
        Vector3 loacPosition = new Vector3(positionX, 0f, positionZ);

        return transform.TransformPoint(loacPosition);
    }
}
