using UnityEngine;

public class RoaderRemover : MonoBehaviour
{
    [SerializeField] private RoadSpawner _road;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Roader roader))
            _road.ReturnRoad(roader);
    }
}
