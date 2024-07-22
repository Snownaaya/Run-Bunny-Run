using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : ObjectPool<Coin>
{
    [SerializeField] private float _delay;
    [SerializeField] private Coin _coin;

    [SerializeField] private float _horizontalMinBounds;
    [SerializeField] private float _horizontalMaxBounds;

    private void Start() => StartCoroutine(CoinGenerator());

    private IEnumerator CoinGenerator()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            Spawn(_coin);
            yield return wait;
        }
    }

    private void Spawn(Coin coin)
    {
        Coin coins = GetObject(coin);
        coins.gameObject.SetActive(true);
        coins.transform.position = RandomCoinPosition();
    }

    private Vector3 RandomCoinPosition()
    {
        float positionX = Random.Range(_horizontalMinBounds, _horizontalMaxBounds);

        float positionZ = 0;

        Vector3 localPosition = new Vector3(positionX, 0f, positionZ);

        return localPosition;
    }
}
