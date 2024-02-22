using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RoadPlaces : MonoBehaviour
{
    [SerializeField] private Roader _tilePrefabs;
    [SerializeField] private float _speed;
    [SerializeField] private int _count;
    [SerializeField] private List<Roader> _listRoad = new List<Roader>();

    private void Awake()
    {
        _listRoad.First().SetSpeed(_speed);

        for (int i = 0; i < _count; i++)
            GenerateTile();
    }

    private void Update()
    {
        if (_listRoad.Count < _count)
            GenerateTile();
    }

    private void GenerateTile()
    {
        Roader roader = Instantiate(_tilePrefabs, _listRoad.Last().transform.position + transform.forward * _tilePrefabs.transform.localScale.z, Quaternion.identity);
        Roader newRoader = roader.GetComponent<Roader>();
        _listRoad.Add(newRoader);
    }
}
