using System.Collections.Generic;
using UnityEngine;

public class PrefabPool : IPrefabPool
{
    private Stack<GameObject> pool;

    private GameObject prefab;
    public int Size { get; private set; }

    public PrefabPool(int size, GameObject prefab)
    {
        Size = size;
        this.prefab = prefab;
        pool = new Stack<GameObject>();
        
        for (int i = 0; i < Size; i++)
        {
            pool.Push(Instantiate());
        }
    }
    
    public void Return(GameObject pooledObject)
    {
        pooledObject.SetActive(false);  
        pool.Push(pooledObject);
    }

    public GameObject Get()
    {
        if (IsEmpty())
        {
            pool.Push(Instantiate());
        }

        GameObject pooledObject = pool.Pop();
        pooledObject.SetActive(true);
        return pooledObject;
    }

    public bool IsEmpty()
    {
        return pool.Count == 0;
    }
    
    private GameObject Instantiate()
    {
        GameObject created = Object.Instantiate(prefab,Vector3.zero, prefab.transform.rotation);
        created.SetActive(false);
        return created;
    }
}
