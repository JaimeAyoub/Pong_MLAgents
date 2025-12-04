using UnityEngine;

public class MovePad : MonoBehaviour
{
    public float velocity;
    public Ball ball;
    public float distanceBallinY;
    public float distanceBallinX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     

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
        var pos = transform.position;
        pos.y += Input.GetAxis("Vertical") * Time.deltaTime * velocity;
        transform.position = pos;
        
     
    }

    void DistanceBall()
    {
        distanceBallinX =  transform.position.x - ball.transform.position.x;
        distanceBallinY =  transform.position.y - ball.transform.position.y;
        
        //MaxDistance in X 16.30 y en Y 9
    }
}