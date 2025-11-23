using UnityEngine;

public class MovePad : MonoBehaviour
{
    private Rigidbody2D rb;
    public float velocity;
    public Ball ball;
    public float distanceBall;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        if (ball == null)
        {
            ball = GameObject.Find("Ball").GetComponent<Ball>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        DistanceBall();
    }

    void Move()
    {
        var pos = rb.position;
        pos.y += Input.GetAxisRaw("Vertical") * Time.deltaTime * velocity;
        rb.position = pos;
    }

    void DistanceBall()
    {
        distanceBall = Vector2.Distance(transform.position, ball.transform.position);
    }
}