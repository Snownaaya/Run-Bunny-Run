using UnityEngine;

public class Roader : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _end;
    [SerializeField] private Transform _begin;

    public Transform Begin => _begin;
    public Transform End => _end;

    private void FixedUpdate() => Move();

    private void Move() => transform.Translate(Vector3.back * _speed * Time.fixedDeltaTime);
}
