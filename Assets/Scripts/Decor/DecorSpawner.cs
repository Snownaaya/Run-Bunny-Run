using UnityEngine;

public class DecorSpawner : MonoBehaviour
{
    [SerializeField] private DecorData _decorData;
    [SerializeField] private Ground _ground;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        if (_decorData.Transform.Count == 0)
            return;

        for (int i = 0; i < _decorData.Count; i++)
        {
            Transform randomPrefab = _decorData.Transform[Random.Range(0, _decorData.Transform.Count)];

            Vector3 randomPosition = _ground.GetRandomPosition();

            Instantiate(randomPrefab, randomPosition, Quaternion.identity);
        }
    }
}
