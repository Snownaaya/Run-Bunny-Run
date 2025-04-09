using System;
using UnityEngine;

public class Roader : MonoBehaviour
{
    [SerializeField] private float _speed = 100f;

    [field: SerializeField] public Transform End { get; protected set; }
    [field: SerializeField] public Transform Begin { get; protected set; }
    [field: SerializeField] public Transform RevivePoint { get; protected set; }

    private ScoreCounter _scoreCounter;
    private Transform _transform;
    private Vector3 _position;

    private float _maxSpeed = 150f;
    private float _currentSpeed = 100f;
    private float _distanceTraveled;

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

        _distanceTraveled += _speed * Time.deltaTime;

        if (_distanceTraveled >= 1f)
        {
            _scoreCounter.IncrementScore();
            _distanceTraveled = 0f;
        }
    }

    public void Initialize(ScoreCounter scoreCounter) =>
        _scoreCounter = scoreCounter;

    public void Reset()
    {
        _speed = 100f;
        _distanceTraveled = 0f;
    }
}