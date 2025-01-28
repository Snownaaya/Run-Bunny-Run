using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private Transform _container;

    private Queue<T> _pool = new Queue<T>();

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

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

    public void Reset()
    {
        foreach (var objectSpawn in _pool.ToList())
        {
            objectSpawn.gameObject.SetActive(false);
        }
        _pool.Clear();
    }
}