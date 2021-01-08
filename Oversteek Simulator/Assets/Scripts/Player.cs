using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class Player : Agent
{
    public float movementSpeed = 1;
    public float rotationSpeed = 300;
    private float rewardFactor = 0.001f;

    private Environment environment;
    private GameObject finish;
    private Rigidbody body;
    private bool isMoving = false;

    public override void Initialize()
    {
        base.Initialize();
        environment = GetComponentInParent<Environment>();
        body = GetComponent<Rigidbody>();
        finish = environment.finish;
    }

    public override void OnEpisodeBegin()
    {
        body.velocity = new Vector3(0, 0, 0);
        environment.ResetEnvironment();
    }

    private void FixedUpdate()
    {
        // Add a reward based on the distance to the finish.
        float distance = Vector3.Distance(transform.localPosition, finish.transform.localPosition);
        if (distance < 25 && isMoving)
        {
            AddReward(rewardFactor / distance);
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Add distance to the finish as observation.
        float distance = Vector3.Distance(transform.localPosition, finish.transform.localPosition);
        sensor.AddObservation(distance);
    }

    public override void Heuristic(float[] actionsOut)
    {
        // Check for forward input.
        if (Input.GetKey(KeyCode.W))
            actionsOut[0] = 1f;
        else
            actionsOut[0] = 0f;

        // Check for rotation input.
        if (Input.GetKey(KeyCode.LeftArrow))
            actionsOut[1] = 1f;
        else if (Input.GetKey(KeyCode.RightArrow))
            actionsOut[1] = 2f;
        else
            actionsOut[1] = 0f;
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        isMoving = false;

        // Apply forward movement.
        if (vectorAction[0] != 0)
        {
            transform.position += transform.forward * movementSpeed * Time.deltaTime * 2;
            isMoving = true;
        }

        // Apply rotation change.
        if (vectorAction[1] != 0)
            transform.Rotate(0, rotationSpeed * (vectorAction[1] * 2 - 3) * Time.deltaTime, 0);
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Finish"))
        {
            AddReward(1);
            EndEpisode();
        }
    }

    public void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Grass"))
        {
            AddReward(-0.0001f);
        }
        else if (collision.CompareTag("Road"))
        {
            AddReward(-0.0002f);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Car"))
        {
            AddReward(-1);
            EndEpisode();
        }
    }
}
