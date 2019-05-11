using UnityEngine;

public interface IPrefabPool
{
    void Return(GameObject pooledObject);
    GameObject Get();
    bool IsEmpty();
}
