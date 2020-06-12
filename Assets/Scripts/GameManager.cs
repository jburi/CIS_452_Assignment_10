/*
* Jacob Buri
* GameManager.cs
* Assignment 10 - Singleton and ObjectPool
* Creates and stores the game prefabs and score
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Prefabs
    public Ball ball;
    public Paddle paddle;
    public Text scoreText;

    //Clones to collect the score
    Ball originalBall;
    Paddle paddleClone1;
    Paddle paddleClone2;

    //Variables
    int score;
    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    // Start is called before the first frame update
    void Start()
    {
        //Convert screen coordinates to the game
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        //Create First Ball - Fixes bug with displaying the score during the game
        originalBall = Instantiate(ball) as Ball;
        originalBall.OnObjectSpawn();

        //Spawn balls every 10 seconds
        InvokeRepeating("BallSpawn", 10, 10);

        //Create paddles
        Paddle paddle1 = Instantiate(paddle) as Paddle;
        Paddle paddle2 = Instantiate(paddle) as Paddle;
        paddle1.Init(true);
        paddle2.Init(false);

        //Paddle clone GameObjects to keep score
        paddleClone1 = paddle1;
        paddleClone2 = paddle2;
    }

    // Update is called once per frame
    void Update()
    {
        //Collect the current score and display it during the game
        score = paddleClone1.GetScore() + paddleClone2.GetScore();
        scoreText.text = score.ToString();
    }

    //Spawn a ball from the ObjectPool
    void BallSpawn()
    {
        ObjectPooler.instance.SpawnFromPool("Ball");
    }

    //Pass the score to Ball.cs
    public int GetScore()
    {
        return score;
    }
}
