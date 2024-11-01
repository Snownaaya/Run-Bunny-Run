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

    public float Speed => _speed;

    private void Awake()
    {
        _scoreCounter = new ScoreCounter();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        if (_coinSpawner != null && Time.time - _lastCoinSpawnTime > _spawnInterval)
        {
            _coinSpawner.Spawn(_coin, transform);
            _lastCoinSpawnTime = Time.time;
        }

        _scoreCounter.IncrementScore(1);
    }

    public void Init(CoinSpawner coinSpawner)
    {
        _coinSpawner = coinSpawner;
    }
}
