/*
* Jacob Buri
* Scoreboard.cs
* Assignment 10 - Singleton and ObjectPool
* List of integers as a singleton to store past scores
*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Scoreboard : MonoBehaviour
{
    //Variables
    public static Scoreboard instance;

    [SerializeField]
    private List<int> scores;

    private Scoreboard(){ }

    /* Singleton Textbook
    public Scoreboard getScoreboard()
    {
        if (instance == null)
        {
            instance = new Scoreboard();
        }

        return instance;
    }
    */

    //Instantiate Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            //My High Score
            scores.Add(50);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Add new score to the scoreboard instance
    public void addScore(int newScore)
    {
        scores.Add(newScore);
        scores.Sort();
        scores.Reverse();
    }

    //Change int to string to be displayed as UI text
    public string ListToText()
    {
        string result = "";
        foreach(int listMember in scores)
        {
            result += listMember.ToString() + "\n";
        }

        return result;
    }
}
