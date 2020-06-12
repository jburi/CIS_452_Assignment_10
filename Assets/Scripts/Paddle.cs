/*
* Jacob Buri
* .cs
* Assignment 10 - Singleton and ObjectPool
* Gives the paddles control inputs. Adds to the score for each colision with a ball.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //Variables
    public float speed;
    public bool isLeft;
    float height;
    int score;

    //Used to change controls - Can implement separate controls for each paddle
    string input = "PaddleLeft";    
    
    void Start()
    {
        //Instantiate Variables
        height = transform.localScale.y;
        speed = 14f;
        score = 0;
    }

    public void Init(bool isLeftPaddle)
    {
        //Determine which paddle is which
        isLeft = isLeftPaddle;
        Vector2 pos = Vector2.zero;

        if (isLeftPaddle)
        {
            //Place on the left of the screen
            pos = new Vector2(GameManager.bottomLeft.x, 0);
            pos += Vector2.right * transform.localScale.x * 2;

            input = "PaddleLeft";
        }
        else
        {
            //Place on the right of the screen
            pos = new Vector2(GameManager.topRight.x, 0);
            pos -= Vector2.right * transform.localScale.x * 2;

            //input = "PaddleRight"; - Not implemented on purpose
        }

        //Assign corredponding position and inputs
        transform.position = pos;
        transform.name = input;
    }

    private void Update()
    {
        //Convert input to a usable float
        float move = Input.GetAxis(input) * Time.deltaTime * speed;

        //Restrict paddles to the screen
        if (transform.position.y < GameManager.bottomLeft.y + height/2 && move < 0)
        {
            move = 0;
        }
        if (transform.position.y > GameManager.topRight.y - height / 2 && move > 0)
        {
            move = 0;
        }

        //Moves the paddle based on input
        transform.Translate(move * Vector2.up);
    }

    //Adds a counter to the score for each time the ball hits the paddle
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            score++;
        }
    }

    //Getter for GameMaster to display the score
    public int GetScore()
    {
        return score;
    }
}
