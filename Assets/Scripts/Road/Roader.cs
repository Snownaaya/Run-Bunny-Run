using System;
using UnityEngine;

public class Roader : MonoBehaviour
{
    [SerializeField] private float _speed = 90f;
    [SerializeField] private ScoreCounter _scoreCounter;

    [field: SerializeField] public AnimationCurve ChanceFromDistance { get; private set; }
    [field: SerializeField] public Transform End { get; protected set; }
    [field: SerializeField] public Transform Begin { get; protected set; }
    [field: SerializeField] public Transform RevivePoint { get; protected set; }
    [field: SerializeField] public Transform[] CoinSpawnPoints { get; protected set; }

    private Transform _transform;
    private Vector3 _position;

    private float _maxSpeed = 140f;
    private float _currentSpeed = 100f;
    private float _distanceTraveled;
    private float _speedScore = 10;

    public float CurrentSpeed
    {
        get => _speed;
        set => _speed = Mathf.Clamp(value, _currentSpeed, _maxSpeed);
    }

    public event Action RoadMoved;

    private void Awake()
    {
        _transform = transform;
        _transform.position = _position;
    }

    private void Update() =>
        Move();

    private void Move()
    {
        float move = _speed * Time.deltaTime;

        _transform.Translate(_transform.forward * move);
        CalculateScore();
        RoadMoved?.Invoke();
    }

    private void CalculateScore()
    {
        if (_scoreCounter == null)
            return;

        _distanceTraveled += _speedScore * Time.deltaTime;

        if (_distanceTraveled >= 1f)
        {
            _scoreCounter.IncrementScore();
            _distanceTraveled = 0f;
        }
    }

    public void Reset()
    {
        _speed = 100f;
        _distanceTraveled = 0f;
    }
}