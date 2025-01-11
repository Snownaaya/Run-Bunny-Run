using UnityEngine;

[CreateAssetMenu(fileName = "Fireflies", menuName = "ScriptableObject/Fireflies", order = 1)]
public class FireflyData : ScriptableObject
{
    [SerializeField] private Fireflies _fireflies;
    [SerializeField] private int _count = 2;

    public Fireflies Fireflies => _fireflies;
    public int Count => _count;
}