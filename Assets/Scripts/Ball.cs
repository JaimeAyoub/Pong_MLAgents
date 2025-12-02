using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public Vector2 speed;
    public Vector2 position;
    public PadelAgent lastWhoTouched;
    private ScoreManager scoreManager;

    public Vector2 Velocity;
    private Vector2 center = new Vector2(0, 0);

    void Start()
    {
        if (scoreManager == null)
            scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

        Reseto();
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
            Velocity.y *= -1;
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2"))
        {
            Debug.Log("Hit");
            var agent = other.gameObject.GetComponent<PadelAgent>();
            lastWhoTouched = agent;
            agent.HitBall();

            // Debug.Log("Cambio");
            Velocity.x *= -1;
            int randomY = Random.Range(1, 3);
            if (randomY == 1)
                Velocity.y *= -1;
        }

        if ((other.gameObject.CompareTag("GolIzquierda") || (other.gameObject.CompareTag("GolDerecha"))))
        {
            // Debug.Log("Gol");
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


    public void Reseto()
    {
        transform.position = center;

        int randomVelocityX = Random.Range(0, 2);
        int randomVelocityY = Random.Range(0, 2);

        if (randomVelocityX == 1)
            Velocity.x *= -1;


        if (randomVelocityY == 1)
            Velocity.y *= -1;
    }

    public void MoveBall()
    {
        position = transform.position;
        position += (speed * Velocity * Time.deltaTime);
        transform.position = position;
    }
}