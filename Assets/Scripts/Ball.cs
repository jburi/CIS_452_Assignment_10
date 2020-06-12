/*
* Jacob Buri
* Ball.cs
* Assignment 10 - Singleton and ObjectPool
* Adds force to the ball and checks if it hits a paddle
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour, IPooledObject
{
    //Variables
    [SerializeField]
    float speed;

    float radius;
    int score;
    Vector2 direction;

    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        direction = Vector2.one.normalized;
        speed = 3;
        score = 0;

        //Ball Width
        radius = transform.localScale.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        //Bounce off the top and Bottom
        if (transform.position.y < GameManager.bottomLeft.y + radius && direction.y < 0)
        {
            direction.y = -direction.y;
        }
        if (transform.position.y > GameManager.topRight.y - radius && direction.y > 0)
        {
            direction.y = -direction.y;
        }

        //Detect if the ball is out of play
        if ((transform.position.x < GameManager.bottomLeft.x + radius && direction.x < 0) || 
            (transform.position.x > GameManager.topRight.x - radius && direction.x > 0))
        {
            //Get Score from GameManager
            GameManager gm = FindObjectOfType<GameManager>();
            score = gm.GetScore();

            //Add score to scoreboard singleton
            Scoreboard.instance.addScore(score);

            //Load Game Over Scene
            SceneManager.LoadScene("GameOver");
        }
    }

    //Paddle Colision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Paddle")
        {
            //Ball goes the opposite direction to emulate a bounce
            bool isLeft = collision.GetComponent<Paddle>().isLeft;

            if (isLeft == true && direction.x < 0)
            {
                direction.x = -direction.x;
            }
            if (isLeft == false && direction.x > 0)
            {
                direction.x = -direction.x;
            }
        }
    }
}
