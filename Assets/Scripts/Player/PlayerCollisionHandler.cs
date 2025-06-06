using UnityEngine;
using System;

[RequireComponent(typeof(Character))]
public class PlayerCollisionHandler : MonoBehaviour
{
    public event Action<IInteractable> CollisionDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
            CollisionDetected?.Invoke(interactable);
    }
}