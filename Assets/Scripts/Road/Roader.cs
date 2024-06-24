using System.Collections;
using UnityEngine;

public class Roader : MonoBehaviour
{
   [field: SerializeField] public Transform End {get; protected set;}
   [field: SerializeField] public Transform Begin { get; protected set; }

    [SerializeField] private float _speed;
    //[SerializeField] private float _delay;

    private ScoreCounter _scoreCounter;

    private void Start() => StartCoroutine(Move());

    public void Init(ScoreCounter scoreCounter) => _scoreCounter = scoreCounter;

    private IEnumerator Move()
    {
        while (enabled)
        {
            //var waitForSecond = new WaitForSeconds(_delay);

            transform.Translate(Vector3.back);
            _scoreCounter?.IncrementScore();
            yield return null;
        }
    }
}
