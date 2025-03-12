using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _ground;
    [SerializeField, Range(0.01f, 1)] private float _checkRaduis;

    public bool IsTouches { get; private set; }

    private void Update() =>
        IsTouches = Physics.CheckSphere(transform.position, _checkRaduis, _ground);
}