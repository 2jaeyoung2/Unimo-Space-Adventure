using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using ZL.Unity;

public abstract class GenericPool<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _poolParent;
    private ObjectPool<T> _pool;

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>(
            CreateFunc,
            OnGet,
            OnRelease,
            OnDstroy,
            false,
            3
            );
    }

    private T CreateFunc()
    {
        var obj = Instantiate(_prefab, _poolParent);
        obj.gameObject.SetActive(false);
        return obj;
    }

    private void OnGet(T obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnRelease(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDstroy(T obj)
    {
        Destroy(obj.gameObject);
    }

    public T Get(Transform parent, Vector3 offset)
    {
        var obj = _pool.Get();
        obj.transform.SetParent(parent, false);
        obj.transform.localPosition = offset;
        return obj;
    }

    public void Release(T obj)
    {
        _pool.Release(obj);
    }

    public void ReleaseAll()
    {
        foreach(Transform child in _poolParent)
        {
            if(child.TryGetComponent(out T obj))
            {
                _pool.Release(obj);
            }
        }
    }
}
