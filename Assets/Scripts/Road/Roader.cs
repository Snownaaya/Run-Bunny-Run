using System;
using UnityEngine;

public class Roader : MonoBehaviour
{
    [SerializeField] private float _currentSpeed = 100;

    private float _maxSpeed = 180f;

    [field: SerializeField] public Transform End { get; protected set; }
    [field: SerializeField] public Transform Begin { get; protected set; }

    private ScoreCounter _scoreCounter;
    private Transform _transform;
    private HandleRoadMovement _handleRoadMovement;

    private float _distanceTraveled;

    public float CurrentSpeed
    {
        get => _currentSpeed;
        set => _currentSpeed = Mathf.Clamp(value, _currentSpeed, _maxSpeed);
    }

    public event Action RoadMoved;

    private void Awake() =>
        _transform = transform;

    private void Update() =>
        Move();

    public void IncreaseSpeed(float increment) =>
        _currentSpeed += increment;

    private void Move()
    {
        float move = _currentSpeed * Time.deltaTime;

        _transform.Translate(_transform.forward * move);
       CalculateScore();
        RoadMoved?.Invoke();
    }

    private void CalculateScore()
    {
        if (_scoreCounter == null)
            return;

        _distanceTraveled += _currentSpeed * Time.deltaTime;

        if (_distanceTraveled >= 1f)
        {
            _scoreCounter.IncrementScore();
            _distanceTraveled = 0f;
        }
    }

    public void Initialize(ScoreCounter scoreCounter) =>
        _scoreCounter = scoreCounter;
}