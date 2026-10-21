using NUnit.Framework;
using System;
using System.Collections.Generic ;
using UnityEditor;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    private List<PooledObject> bulletPool = new List<PooledObject>();
    private List<PooledObject> rocketPool = new List<PooledObject>();
    //public List<PooledObject> objectPool = new List<PooledObject>();
    private List<PooledObject> enemyBulletPool = new List<PooledObject>();
    public List<PooledObject> usedPool = new List<PooledObject>();

    // - Object Pool Count
    [SerializeField] int numberOfObjects = 5;

    // - Object Pooling Challenge 
    // - Create a two object pools specifically for rocket and bullets. Modify your GetPooledObjects method

    // - Reference Object to create 
    //public GameObject objectToCreate;
    public GameObject bulletPrefab;
    public GameObject rocketPrefab;
    public GameObject enemyBulletPrefab;
    private void Awake()
    {
        // - Singleton pattern
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else if (Instance == null)
        {
            Instance = this;
        }

        // - Initialize our object pool
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            AddNewObject(bulletPrefab, bulletPool);
            AddNewObject(rocketPrefab, rocketPool);
            AddNewObject(enemyBulletPrefab, enemyBulletPool);
        }
    }

    private void AddNewObject(GameObject prefab, List<PooledObject> targetPool)
    {
        if (prefab == null) return;
        // - Create a new object for our object 
        GameObject newObject = Instantiate(prefab, transform.position, Quaternion.identity);
        PooledObject pooledObj = newObject.GetComponent<PooledObject>();

        pooledObj.SetObjectPool(this);
        newObject.SetActive(false);

        targetPool.Add(pooledObj);


        // - Set the object pool reference in the pooled object so it can return itself when "destroyed"
        //newObject.GetComponent<PooledObject>().SetObjectPool(this);
        //// - Hide the object on creation, we are not using it. It is available for us
        //newObject.SetActive(false);
        //// - Add the new object to the object pool list (availabel)
        //objectPool.Add(newObject.GetComponent<PooledObject>());
    }
    public PooledObject GetPooledObject(string objectType)
    {
        List<PooledObject> selectedPool;

        if (objectType == "Rocket")
        {
            selectedPool = rocketPool;
        }
        else if (objectType == "EnemyBullet")
        {
            selectedPool = enemyBulletPool;
        }
        else
        {
            selectedPool = bulletPool;
        }
        
        if (selectedPool.Count > 0)
        {
            PooledObject obj = selectedPool[0];
            selectedPool.RemoveAt(0);
            usedPool.Add(obj);

            obj.gameObject.SetActive(true);
            return obj;
        }
        //public PooledObject GetPooledObject()
        //{
        //    // - Checks for an object
        //    if (objectPool.Count > 0)
        //    {
        //        usedPool.Add(objectPool[0]);
        //        objectPool.RemoveAt(0); 
        //        // - Activate and return to caller
        //        usedPool[usedPool.Count - 1].gameObject.SetActive(true);
        //        return usedPool[usedPool.Count - 1];
        //    }

        else
        {
            GameObject prefabToUse = (objectType == "Rocket") ? rocketPrefab : bulletPrefab;
            Debug.Log("No POOLED OBJECT");
            return null;
        }
    }
    public void RestoreObject(PooledObject _pooledObject)
    {
        _pooledObject.gameObject.SetActive(false);
        usedPool.Remove(_pooledObject);

        string objectName = _pooledObject.gameObject.name;

        // Put it back in the correct pool by checking its name or tag
        if (objectName.Contains("Rocket"))
        {
            rocketPool.Add(_pooledObject);
        }
        else if (objectName.Contains("EnemyBullet"))
        {
            enemyBulletPool.Add(_pooledObject);
        }
        else
        {
            bulletPool.Add(_pooledObject);
        }
        //objectPool.Add(_pooledObject);
        //usedPool.Remove(_pooledObject);
        //_pooledObject.gameObject.SetActive(false);
    }
}
