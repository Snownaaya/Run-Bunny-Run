using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Roader : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private Coin _coin;

    [field: SerializeField] public Transform End { get; protected set; }
    [field: SerializeField] public Transform Begin { get; protected set; }

    private ScoreCounter _scoreCounter;
    private CoinSpawner _coinSpawner;

    private float _spawnInterval = 2f;
    private float _lastCoinSpawnTime = 0f;

    private bool _isMove = true;

    public float Speed => _speed;

    private void Start()
    {
        StartCoroutine(Move());
    }

    public void Init(ScoreCounter scoreCounter, CoinSpawner coinSpawner)
    {
        _scoreCounter = scoreCounter;
        _coinSpawner = coinSpawner;
    }

    private IEnumerator Move()
    {
        while (_isMove)
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);

            if (_coinSpawner != null && Time.time - _lastCoinSpawnTime > _spawnInterval)
            {
                _coinSpawner.Spawn(_coin, transform);
                _lastCoinSpawnTime = Time.time;
            }

            _scoreCounter?.IncrementScore();
            yield return null;
        }
    }
}
