using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestRoadTracker : MonoBehaviour
{
    [SerializeField] private RoaderStorage _roaderStorage;
    [SerializeField] private float _updateInterval = 1f;

    private Transform _trackedObject;
    private Roader _closestRoad;
    private Transform _lastRevivePoint;
    private Coroutine _trackingCoroutine;

    public Roader ClosestRoad => _closestRoad;
    public Transform LastRevivePoint => _lastRevivePoint;

    public void StartTracking(Transform target)
    {
        _trackedObject = target;

        if (_trackingCoroutine != null)
            StopCoroutine(_trackingCoroutine);

        _trackingCoroutine = StartCoroutine(UpdateClosestRoadRoutine(_roaderStorage.ActiveRoads[0]));
    }

    public void StopTracking()
    {
        if (_trackingCoroutine != null)
        {
            StopCoroutine(_trackingCoroutine);
            _trackingCoroutine = null;
        }
    }

    public void SetLastRevivePoint(Transform revivePoint) =>
        _lastRevivePoint = revivePoint;

    private Roader FindClosestRoad()
    {
        IReadOnlyList<Roader> roads = _roaderStorage.ActiveRoads;

        if (roads == null || roads.Count == 0)
            return null;

        Roader closest = null;
        float minDistance = float.MaxValue;
        Vector3 trackedPos = _trackedObject.position;

        foreach (Roader road in roads)
        {
            float distance = Vector3.Distance(trackedPos, road.RevivePoint.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closest = road;
            }
        }

        if (closest == null)
            closest = roads[0];

        return closest;
    }

    private IEnumerator UpdateClosestRoadRoutine(Roader roader)
    {
        while (enabled)
        {
            if (_trackedObject != null)
            {
                roader = _closestRoad;
                _closestRoad = FindClosestRoad();
            }

            yield return new WaitForSeconds(_updateInterval);
        }
    }
}