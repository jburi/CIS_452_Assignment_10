/*
* Jacob Buri
* LoadScene.cs
* Assignment 10 - Singleton and ObjectPool
* Used to switch between scenes
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadSceneOnClick(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
