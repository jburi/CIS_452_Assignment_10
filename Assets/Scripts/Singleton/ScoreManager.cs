/*
* Jacob Buri
* ScoreManager.cs
* Assignment 10 - Singleton and ObjectPool
* Converts the list of scores to display on the UI
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //Variables
    string theScores;
    public Text scoreboardText;

    void Update()
    {
        //Conversion
        theScores = Scoreboard.instance.ListToText();

        //Display
        scoreboardText.text = theScores;
    }
}
