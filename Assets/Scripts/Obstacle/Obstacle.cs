using UnityEngine;
using DG.Tweening;

public class Obstacle : MonoBehaviour, IInteractable
{
    [SerializeField] private float _speedReduction;

    private HandleRoadSpeed _roadSpeed;
    private Tween _animation;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;

        _animation = _transform
            .DOShakePosition(0.1f, 0.1f)
            .SetAutoKill(false);

        _roadSpeed = FindObjectOfType<HandleRoadSpeed>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character player))
        {
            _roadSpeed.DecreaseSpeed(_speedReduction);
            _animation.Restart();
        }
    }
}