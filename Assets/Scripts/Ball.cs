using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();

        SetUp();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBall();
    }

    void SetUp()
    {
        DirectionStart();
    }

    void DirectionStart()
    {
        int randomPosX = Random.Range(1, 3);
        if (randomPosX == 1)
            speed.x = randomPosX * -10;
        else if (randomPosX == 2)
            speed.x = randomPosX * 10;

        int randomPosY = Random.Range(1, 3);
        if (randomPosY == 1)
            speed.y = randomPosY * -10;
        else if (randomPosY == 2)
            speed.y = randomPosY * 10;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ChangeDirection(other.gameObject.tag);
    }

    void ChangeDirection(String tag)
    {
        if (tag == "GolIzquierda")
            speed.x = -speed.x;
        else if (tag == "GolDerecha")
            
        if (tag == "WallY")
            speed.y = -speed.y;
        if (tag == "Player")
        {
            speed = ChangeVelocityRandom();
            speed.x = -speed.x;
            int randomY = Random.Range(1, 3);
            if (randomY == 1)
                speed.y = -speed.y;
            else if (randomY == 2)
                speed.y = speed.y;
        }
    }

    void MoveBall()
    {
        rb.linearVelocity = speed;
        Mathf.Clamp(rb.linearVelocityX, -20, 20);
        Mathf.Clamp(rb.linearVelocityY, -20, 20);
    }

    Vector2 ChangeVelocityRandom()
    {
        int speedX = Random.Range(5, 16);
        int speedY = Random.Range(5, 16);
        return new Vector2(speedX, speedY);
    }
}