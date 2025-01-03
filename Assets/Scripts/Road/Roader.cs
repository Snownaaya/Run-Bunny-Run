using System;
using UnityEngine;

public class Roader : MonoBehaviour
{
    [SerializeField] private float _speed;

    [field: SerializeField] public Transform End { get; protected set; }
    [field: SerializeField] public Transform Begin { get; protected set; }

    private ScoreCounter _scoreCounter;
    private Transform _transform;
    private Vector3 _startPosition;

    private float _distanceTraveled;

    public event Action RoadMoved;

    private void Awake() =>
        _transform = transform;

    private void Update()
    {
        Move();
    }

    public void IncreaseSpeed(float increment)
    {
        _speed += increment;
    }

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

    internal void Initialize(ScoreCounter scoreCounter)
    {
        _scoreCounter = scoreCounter;
    }
}
