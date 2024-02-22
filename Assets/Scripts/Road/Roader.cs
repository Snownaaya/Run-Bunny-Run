using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roader : MonoBehaviour
{
   [SerializeField] private float _speed;

    private void FixedUpdate() => Move();

    public void SetSpeed(float speed) => _speed = speed;

    private void Move() => transform.Translate(Vector3.back * _speed * Time.fixedDeltaTime);
}
