using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public ObjectPool poolReference;
    internal ObjectPool objectType;

    public void SetObjectPool(ObjectPool _pool)
    {
        poolReference = _pool;  
    }
    public void DestroyWithTime(float time)
    {
        Invoke("ResetObject", time);
    }
    public void ResetObject()
    {
       poolReference.RestoreObject(this);
    }
}
