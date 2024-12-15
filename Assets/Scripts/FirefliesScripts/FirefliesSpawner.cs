using System.Collections.Generic;
using UnityEngine;

public class FirefliesSpawner : ObjectPool<Fireflies>, IResetteble
{
    [SerializeField] private Fireflies _fireflies;
    [SerializeField] private List<Transform> _points = new List<Transform>();

    private void Start()
    {
        _points.Add(transform);
        Spawn();
    }

    public void Spawn()
    {
        foreach (Transform point in _points)
        {
            Fireflies fireflies = GetObject(_fireflies);
            fireflies.transform.position = point.position;
        }
    }

    private void RerturnObjectFireflies(Fireflies fireflies)
    {
        ReturnObject(fireflies);
    }
}