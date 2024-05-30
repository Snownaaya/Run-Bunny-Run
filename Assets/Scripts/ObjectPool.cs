using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class ObjectPool<T> : MonoBehaviour, IResetteble where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _container;

    private Queue<T> _pool;

    public IEnumerable<T> PoolObject { get; private set; }

    private void Awake()
    {
        _pool = new Queue<T>();
        PoolObject = _pool;
    }

    public T GetObject()
    {
        if (_pool.Count == 0)
        {
            T newObject = Instantiate(_prefab, _container);
            return newObject;
        }

        T pooledObject = _pool.Dequeue();
        pooledObject.gameObject.SetActive(false);
        return pooledObject;
    }

    public void ReturnObject(T newObject)
    {
        newObject.gameObject.SetActive(false);
        _pool.Enqueue(newObject);
    }

    public void Reset()
    {
        foreach (var objectSpawn in _pool.ToList())
        {
            objectSpawn.gameObject.SetActive(false);
        }

        _pool.Clear();
    }
}