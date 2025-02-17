using UnityEngine;
using Zenject;

public class Obstacle : MonoBehaviour, IInteractable
{
    [SerializeField] private float _speedReduction;

    private HandleRoadSpeed _roadSpeed;

    private void Awake()
    {
        _roadSpeed = FindObjectOfType<HandleRoadSpeed>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            _roadSpeed.DecreaseSpeed(_speedReduction);
    }
}