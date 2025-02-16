using System.Runtime.InteropServices;
using UnityEngine;

public class Decor : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _transform;

    private void Awake() =>
        _transform = transform;

    private void Update() =>
        Move();

    private void Move() =>
        _transform.Translate(_transform.forward * _speed * Time.deltaTime);
}