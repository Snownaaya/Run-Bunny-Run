using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class DecorSpawner : ObjectPool<Decor>
{
    [SerializeField] private Decor[] _decor;
    [SerializeField] private Water _ground;

    private float _spawnInterval = 10f;
    private int _maxDecorCount = 4;

    private List<Decor> _activeDecor = new List<Decor>();

    private void Start() =>
        StartCoroutine(Generate());

    private IEnumerator Generate()
    {
        var wait = new WaitForSeconds(_spawnInterval);

        while (enabled)
        {
            Spawn(_ground);
            yield return wait;
        }
    }

    public void RemoveDecor(Decor decorToRemove)
    {
        ReturnObject(decorToRemove);
        _activeDecor.Remove(decorToRemove);
    }

    private void Spawn(Water currentGround)
    {
        HashSet<int> usedIndices = new HashSet<int>();

        while (_activeDecor.Count < _maxDecorCount && usedIndices.Count < _ground.Points.Length)
        {
            int randomPointIndex;

            do
                randomPointIndex = Random.Range(0, _ground.Points.Length);
            while (usedIndices.Contains(randomPointIndex));

            usedIndices.Add(randomPointIndex);
            Transform spawnPoint = _ground.Points[randomPointIndex];

            Decor randomDecore = _decor[Random.Range(0, _decor.Length)];
            Decor decor = GetObject(randomDecore);

            decor.transform.position = spawnPoint.position;
            _activeDecor.Add(decor);
        }
    }

    public void ClearAllDecor()
    {
        while (_activeDecor.Count > 0)
            RemoveDecor(_activeDecor[0]);
    }
}