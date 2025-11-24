using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 speed;
    public PadelAgent lastWhoTouched;
    private ScoreManager scoreManager;
    private Vector2 center = new Vector2(0, 0);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();
        if (scoreManager == null)
            scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBall();
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log(other.gameObject.tag);

        if (other.gameObject.CompareTag("WallY"))
            speed.y *= -1;
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2"))
        {
            var agent = other.gameObject.GetComponent<PadelAgent>();
            lastWhoTouched = agent;
            agent.HitBall();

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
            if (lastWhoTouched != null)
            {
                // scoreManager.AddScore(lastWhoTouched);
                lastWhoTouched.AddReward(+1.0f);

                foreach (var agent in FindObjectsOfType<PadelAgent>())
                {
                    if (agent != lastWhoTouched)
                        agent.MissBall();
                }

                Reseto();
            }
        }
    }


    void MoveBall()
    {
        rb.linearVelocity = speed;
        if (speed.x == 0)
            speed.x = 20;
    }

    public IEnumerator ResetBall()
    {
        speed = Vector2.zero;
        rb.transform.position = center;

        int randomVelocityX = Random.Range(-20, 21);
        int randomVelocityY = Random.Range(-5, 6);


        yield return new WaitForSeconds(2.0f);
        speed.x = randomVelocityX;
        speed.y = randomVelocityY;
    }

    public void Reseto()
    {
        speed = Vector2.zero;
        rb.transform.position = center;

        int randomVelocityX = Random.Range(-20, 21);
        int randomVelocityY = Random.Range(-5, 6);


        speed.x = randomVelocityX;
        speed.y = randomVelocityY;
    }

    void Reset()
    {
        StopAllCoroutines();
        StartCoroutine(ResetBall());
    }
}