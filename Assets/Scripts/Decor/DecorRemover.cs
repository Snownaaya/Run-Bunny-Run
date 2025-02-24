using UnityEngine;

public class DecorRemover : MonoBehaviour
{
    [SerializeField] private DecorSpawner _decorSpawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Decor decor))
            _decorSpawner.RemoveDecor(decor);
    }
}