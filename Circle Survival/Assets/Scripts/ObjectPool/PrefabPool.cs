using System.Collections.Generic;
using UnityEngine;

public class PrefabPool : IPrefabPool
{
    private Stack<GameObject> _pool;

    private GameObject _prefab;
    public int Size { get; private set; }

    public PrefabPool(int size, GameObject prefab)
    {
        Size = size;
        _prefab = prefab;
        _pool = new Stack<GameObject>();
        
        for (int i = 0; i < Size; i++)
        {
            _pool.Push(Instantiate());
        }
    }
    
    public void Return(GameObject pooledObject)
    {
        pooledObject.SetActive(false);  
        _pool.Push(pooledObject);
    }

    public GameObject Get()
    {
        if (IsEmpty())
        {
            _pool.Push(Instantiate());
        }

        GameObject pooledObject = _pool.Pop();
        pooledObject.SetActive(true);
        return pooledObject;
    }

    public bool IsEmpty()
    {
        return _pool.Count == 0;
    }
    
    private GameObject Instantiate()
    {
        GameObject created = Object.Instantiate(_prefab,Vector3.zero, _prefab.transform.rotation);
        created.SetActive(false);
        return created;
    }
}
