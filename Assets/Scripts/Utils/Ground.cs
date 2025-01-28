using UnityEngine;

public class Ground : MonoBehaviour
{
    private const string Position = nameof(Position);

    [SerializeField] private Vector3 _size;

    [Header(Position)]
    [SerializeField] private int _verticalPosition;
    [SerializeField] private int _horintalPosition;

    public Vector3 GetRandomPosition()
    {
        float positionX = Random.Range(-_size.x / _horintalPosition, _size.x / _verticalPosition);
        float positionZ = Random.Range(-_size.z / _horintalPosition, _size.x / _verticalPosition);

        var randomPosition = transform.position + new Vector3(positionX, 0.2f, positionZ);

        return randomPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(_size.x, 0.2f, _size.z));
    }
}
