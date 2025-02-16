using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour, IPoolObject<T> where T : MonoBehaviour
{
    [SerializeField] private Transform _container;

    private Queue<T> _pool = new Queue<T>();

    public T GetObject(T prefab)
    {
        if (_pool.Count > 0)
        {
            T obj = _pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        T newObj = Instantiate(prefab, _container);
        return newObj;
    }

    public void ReturnObject(T @object)
    {
        @object.gameObject.SetActive(false);
        _pool.Enqueue(@object);
    }

    public int GetActiveCount() =>
        _pool.Count;

    public void ClearPool()
    {
        if (GetActiveCount() > 0)
            _pool.Clear();
    }
}