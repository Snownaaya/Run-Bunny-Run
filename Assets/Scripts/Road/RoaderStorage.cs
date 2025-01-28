using System.Collections.Generic;
using UnityEngine;

public class RoaderStorage : MonoBehaviour
{
    [SerializeField] private List<Roader> _activeRoads = new List<Roader>();

    public IReadOnlyList<Roader> ActiveRoads => _activeRoads;

    public void AddRoad(Roader road)
    {
        if (!_activeRoads.Contains(road))
        {
            _activeRoads.Add(road);
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }

    public void RemoveRoad(Roader road)
    {
        if (_activeRoads.Contains(road))
        {
            _activeRoads.Remove(road);
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }

    public void ResetStorage()
    {
        _activeRoads.Clear();
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
