using System.Collections;
using UnityEngine;

public class CoinSpawner : PoolCoin<Coin>
{
    [SerializeField] private float _delay;

    [SerializeField] private int _horizontalBounds;
    [SerializeField] private int _verticalBounds;

    private Coroutine _coroutine;

    private void Start()
    {
        _coroutine = StartCoroutine(CoinGenerator());
    }


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
        Coin coin = GetCoin();
        coin.transform.position = RandomCoinPosition();
        coin.gameObject.SetActive(true);
    }

    private Vector3 RandomCoinPosition()
    {
        float positionX = RandomGenerator.Range(_horizontalBounds, _verticalBounds);
        float positionZ = RandomGenerator.Range(_horizontalBounds, _verticalBounds); 

        return new Vector3(positionX, transform.position.y, positionZ);
    }
}
