using System.Collections;
using UnityEngine;

public class Roader : MonoBehaviour
{
    [SerializeField] private float _speed;

    [field: SerializeField] public Transform End { get; protected set; }
    [field: SerializeField] public Transform Begin { get; protected set; }

    private ScoreCounter _scoreCounter;

    private bool _isMove = true;

    private void Start() => StartCoroutine(Move());

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
}
