using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 speed;
    public GameObject lastWhoTouched;
    private ScoreManager scoreManager;
    private Vector2 center = new Vector2(0, 0);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();
        if (scoreManager == null)
            scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

        // SetUp();
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
        Debug.Log(other.gameObject.tag);

        if (other.gameObject.CompareTag("WallY"))
            speed.y *= -1;
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2"))
        {
            lastWhoTouched = other.gameObject;
            int randomVelocityY = Random.Range(1, 10);
            Debug.Log("Cambio");
            speed.x *= -1;
            int randomY = Random.Range(1, 3);
            if (randomY == 1)
                speed.y = -1 * randomVelocityY;
            else if (randomY == 2)
                speed.y = randomVelocityY;
        }

        if ((other.gameObject.CompareTag("GolIzquierda") || (other.gameObject.CompareTag("GolDerecha"))))
        {
            Debug.Log("Gol");
            scoreManager.AddScore(lastWhoTouched);
            StartCoroutine(ResetBall());
        }
    }


    void MoveBall()
    {
        rb.linearVelocity = speed;
        Mathf.Clamp(rb.linearVelocityX, -20, 20);
        Mathf.Clamp(rb.linearVelocityY, -20, 20);
    }

    IEnumerator ResetBall()
    {
        speed = Vector2.zero;
        rb.MovePosition(center);

        int randomVelocityX = Random.Range(-10, 11);
        int randomVelocityY = Random.Range(-5, 6);


        yield return new WaitForSeconds(2.0f);
        speed.x = randomVelocityX;
        speed.y = randomVelocityY;
    }

    void Reset()
    {
        StartCoroutine(ResetBall());
        
    }
}