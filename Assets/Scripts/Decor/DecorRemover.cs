using UnityEngine;

public class DecorRemover : MonoBehaviour
{
    [SerializeField] private DecorSpawner _decorSpawner;
    [SerializeField] private Transform _character;
    [SerializeField] private float _zOffset;

    private void Update()
    {
        Vector3 removerPosition = transform.position;
        removerPosition.z = _character.transform.position.z + _zOffset;
        transform.position = removerPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Decor decor))
            _decorSpawner.RemoveDecor(decor);
    }
}