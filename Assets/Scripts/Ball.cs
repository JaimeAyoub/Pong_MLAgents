using System.Threading;
using UnityEngine;
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
        // if (scoreManager == null)
        //     scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

        Reseto();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBall();
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("WallY"))
            Velocity.y *= -1;
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2"))
        {
            var agent = other.gameObject.GetComponent<PadelAgent>();
            lastWhoTouched = agent;
            //Debug.Log(lastWhoTouched);
            agent.HitBall();

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
                    //Debug.Log("Golazo");
                    if (agent != lastWhoTouched)
                        agent.MissBall();
                }

                Reseto();
            }
            else
            {
                foreach (var agent in FindObjectsOfType<PadelAgent>())
                {
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
        lastWhoTouched = null;
    }

    public void MoveBall()
    {
        position = transform.position;
        position += (speed * Velocity * Time.deltaTime);
        transform.position = position;
    }
}