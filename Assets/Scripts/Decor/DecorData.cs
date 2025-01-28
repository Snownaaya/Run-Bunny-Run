using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decore", menuName = "ScriptableObject/Decor", order = 1)]
public class DecorData : ScriptableObject
{
    [SerializeField] private List<Transform> _transform = new List<Transform>();
    [SerializeField] private int _count;
    [SerializeField] private string _name;

    public List<Transform> Transform => _transform;
    public int Count => _count;
    public string Name => _name;
}
