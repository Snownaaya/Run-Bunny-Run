using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class ObjectPool<T> : MonoBehaviour, IResetteble where T : MonoBehaviour
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

        T newObj = Instantiate(prefab);
        return newObj;
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