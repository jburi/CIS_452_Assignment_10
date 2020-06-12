/*
* Jacob Buri
* ObjectPooler.cs
* Assignment 10 - Singleton and ObjectPool
* Stores pools of objects in a dictionary. These pools are queues of GameObjects
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    //Use ObjectPooler in the GameManager
    #region Singleton
    public static ObjectPooler instance;
    private ObjectPooler() { }

    private void Awake()
    {
        instance = this;
    }
    #endregion

    //ObjectPool class
    [Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    //Variables
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {
        //In case of more than one ObjectPool
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        //Parse through Dictionary
        foreach (Pool pool in pools)
        {
            //Create a Queue for each ObjectPool
            Queue<GameObject> objectPool = new Queue<GameObject>();

            //parse through each ObjectPool Queue
            for (int i = 0; i < pool.size; i++)
            {
                //Add GameObjects to the Queue and store them as inactive
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            //Add the pool to the Dictionary
            poolDictionary.Add(pool.tag, objectPool);
        }  
    }

    public GameObject SpawnFromPool(string tag)
    {
        //Error handling for invaild tag
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        //Remove from Queue and set as active
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);

        //Spawn PooledObject
        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        //Recycle objects if the queue is empty
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }    
}
