using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    [SerializeField] Player _player;

    private float _distance = 0.7f;
    private float _maxDistance = 6.541f;

    private void Awake() => _player = FindObjectOfType<Player>();

    private void FixedUpdate()
    {
        if (_player.transform.position.z >= transform.position.z && transform.position.z <= _maxDistance)
        {
            transform.Translate(_player.transform.position.z * Time.fixedDeltaTime, 0, 0);
        }

        if (_player.transform.position.z >= transform.position.z && _player.transform.position.z <= transform.position.z)
        {
            transform.Translate(-_player.transform.position.z * Time.fixedDeltaTime, 0, 0);
        }

        if (_player.transform.position.z == _distance)
        {
            transform.Translate(_player.transform.position.z, 0, 0);
        }
    }
}
