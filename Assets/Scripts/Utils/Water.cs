using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Character _character;
    [SerializeField] private HandleRoadSpeed _roadSpeed;
    [SerializeField] private float _zOffset = -100f;

    public Transform[] Points => _points;

    private void Update()
    {
        Vector3 waterPosition = transform.position;
        waterPosition.z = _character.transform.position.z + _zOffset;
        transform.position = waterPosition;
    }

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = transform.childCount;
        _points = new Transform[pointCount];

        for (int i = 0; i < pointCount; i++)
            _points[i] = transform.GetChild(i);
    }
#endif
}
