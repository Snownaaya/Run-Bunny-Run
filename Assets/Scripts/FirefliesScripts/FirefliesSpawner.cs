using System.Collections;
using UnityEngine;

namespace Assets.Scripts.FirefliesScripts
{
    internal class FirefliesSpawner : ObjectPool<Fireflies>
    {
        [SerializeField] private FireflyData _data;
        [SerializeField] private Transform[] _points;

        private void Start() =>
            StartCoroutine(SpawnFireflies());

        public IEnumerator SpawnFireflies()
        {
            foreach (var point in _points)
            {
                Spawn(point.position);
                yield return null;
            }
        }

        public void Return()
        {
            ReturnObject(_data.Fireflies);
        }

        private void Spawn(Vector3 position)
        {
            Fireflies fireflies = GetObject(_data.Fireflies);
            fireflies.transform.position = position;
        }
    }
}
