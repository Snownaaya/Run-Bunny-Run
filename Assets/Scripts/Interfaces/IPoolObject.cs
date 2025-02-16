using UnityEngine;

public interface IPoolObject<T> where T : MonoBehaviour
{
    T GetObject(T prefab);
    void ReturnObject(T obj);
    int GetActiveCount();
    void ClearPool();
}
