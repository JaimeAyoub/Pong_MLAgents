using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PadelAgent : Agent
{
    Rigidbody2D rBody;
    public Ball Target;
    public float velocity;


    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        Target = FindAnyObjectByType<Ball>();
    }

    override public void OnEpisodeBegin()
    {
        // Empezamos de nuevo
        Target.Reseto();
        transform.position = this.gameObject.CompareTag("Player") ? new Vector2(8, 0) : new Vector2(-8, 0);
    }

    override public void CollectObservations(VectorSensor sensor)
    {
        // Pad 
        sensor.AddObservation(transform.position.y);

        // Pelota posición
        sensor.AddObservation(Target.transform.position.x);
        sensor.AddObservation(Target.transform.position.y);

        // Pelota velocidad
        sensor.AddObservation(Target.speed.x);
        sensor.AddObservation(Target.speed.y);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 1
        Vector2 controlSignal = Vector2.zero;
        controlSignal.y = actionBuffers.ContinuousActions[0];
        var pos = rBody.position;
        pos.y += controlSignal.y * Time.deltaTime * velocity;
        rBody.position = pos;


        // Salio de bordes
        if (transform.position.y < -4.0f || transform.position.y > 4.0f) //Se salio de los bordes
        {
            EndEpisode();
        }
    }

    public void HitBall()
    {
        AddReward(+0.2f); // Golpea la pelota
    }

    public void MissBall()
    {
        AddReward(-1f); // Gol
        EndEpisode();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical");
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    //mlagents-learn Pad.yaml --run-id=Pad0
}