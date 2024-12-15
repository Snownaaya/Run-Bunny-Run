using UnityEngine;

public class CameraOffset : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Vector3 _offset;

    private void Awake() => _offset = transform.position - _player.transform.position;
}
