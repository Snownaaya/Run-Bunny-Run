using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] Coin _coin;
    [SerializeField] List<Transform> _points = new List<Transform>();
    [SerializeField] private float _nextCoolDown = 4f;
    [SerializeField] private Transform _coinParent;

    private Coroutine _coroutine;
    private Vector3 _offsetCoin;

    private float _nextSpawnCoin = 1f;

    private void Awake() => StartCoroutine(Spawn());

    private IEnumerator Spawn()
    {
        _nextSpawnCoin = Time.time + _nextSpawnCoin;

        while (Time.time < _nextSpawnCoin)
        {
            int randomIndex = RandomGenerator.Range(0, _points.Count);
            Vector3 spawnPosition = _points[randomIndex].position + _offsetCoin;

            Coin newCoin = Instantiate(_coin, spawnPosition, Quaternion.identity);
            newCoin.transform.SetParent(_coinParent);

            yield return null;
        }
    }
}
