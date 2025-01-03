using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoaderStorage : MonoBehaviour
{
    [SerializeField] private List<Roader> _activeRoaders = new List<Roader>();

    private float _speedIncreaseInterval = 1f;
    private float _speedIncreaseAmount = 1f;

    public IReadOnlyList<Roader> ActiveRoads => _activeRoaders;

    private void Start()
    {
        StartCoroutine(IncreasRpadSpeed());
    }

    public void AddRoad(Roader roader) =>
        _activeRoaders.Add(roader);

    public void RemoveRoad(Roader roader)
    {
        _activeRoaders.Remove(roader);
    }

    private IEnumerator IncreasRpadSpeed()
    {
        var wait = new WaitForSeconds(_speedIncreaseInterval);
        yield return wait;

        //foreach (var roader in _activeRoaders)
        //{
        //    roader.IncreaseSpeed(_speedIncreaseAmount);
        //}

        StartCoroutine(IncreasRpadSpeed());

    }
}
