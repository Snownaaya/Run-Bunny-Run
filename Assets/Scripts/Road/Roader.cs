using System.Collections;
using UnityEngine;
using System;

public class Roader : MonoBehaviour, IResetteble
{
    [SerializeField] private float _speed;

    [field: SerializeField] public Transform End { get; protected set; }
    [field: SerializeField] public Transform Begin { get; protected set; }

    private ScoreCounter _scoreCounter;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private bool _isMove = true;

    private void Start()
    {
        StartCoroutine(Move());

        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    public void Init(ScoreCounter scoreCounter) => _scoreCounter = scoreCounter;

    private IEnumerator Move()
    {
        while (_isMove)
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            _scoreCounter?.IncrementScore();
            yield return null;
        }
    }

    public void Reset()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
    }
}
