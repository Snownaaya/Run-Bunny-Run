using UnityEngine;
using System;

public class Roader : MonoBehaviour
{
    [SerializeField] private Transform _end;
    [SerializeField] private Transform _begin;

    [SerializeField] private float _speed;

    private int _roadIndex = 0;

    public event Action<int> RoadMoving;

    public int ScoreChange { get; private set; }

    public Transform Begin => _begin;
    public Transform End => _end;

    private void Update() => Move();

    private void Move()
    {
        ScoreChange++;
        RoadMoving?.Invoke(_roadIndex);

        transform.Translate(Vector3.back * _speed * Time.fixedDeltaTime);
    }
}
