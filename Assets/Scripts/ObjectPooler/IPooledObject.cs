/*
* Jacob Buri
* IPooledObject.cs
* Assignment 10 - Singleton and ObjectPool
* Interface for a pooled object. Called in ObjectPooler.cs
*/
using UnityEngine;

public interface IPooledObject
{
    void OnObjectSpawn();
}
