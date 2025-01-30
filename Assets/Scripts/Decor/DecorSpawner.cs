//using UnityEngine;

//public class DecorSpawner : ObjectPool<Decor>
//{
//    [SerializeField] private DecorData _decorData;
//    [SerializeField] private Ground _ground;

//    private void Start()
//    {
//        Spawn();
//    }

//    public void Spawn()
//    {
//        if (_decorData.Transform.Count == 0)
//            return;

//        for (int i = 0; i < _decorData.Count; i++)
//        {
//            Transform randomPrefab = _decorData.Transform[Random.Range(0, _decorData.Transform.Count)];

//            Decor decor = GetObject(randomPrefab.GetComponent<Decor>());

//            if (decor == null)
//            {
//                decor = Instantiate(randomPrefab, _ground.GetRandomPosition(), Quaternion.identity)
//                    .gameObject.AddComponent<Decor>();
//            }

//            decor.Setup(_ground.GetRandomPosition());
//        }
//    }
//}
