using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _speedReduction = 1f;

    private HandleRoadMovement _handleRoadMovement;

    public void Initialize(HandleRoadMovement handleRoadMovement)
    {
        _handleRoadMovement = handleRoadMovement;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _handleRoadMovement?.DecreaseSpeed(_speedReduction);
        }
    }
}