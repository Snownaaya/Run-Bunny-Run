using UnityEngine;

[CreateAssetMenu(fileName = "RoadData", menuName = "ScriptableObject/Roader", order = 1)]
public class RoadData : ScriptableObject
{
    [SerializeField] private Roader _roadPrefab;
    [SerializeField] private float _count;

    public Roader RoadPrefab => _roadPrefab;
    public float Count => _count;
}
