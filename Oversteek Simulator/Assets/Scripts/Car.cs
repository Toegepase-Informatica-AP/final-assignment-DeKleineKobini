using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public abstract class Car : Agent
{
    public float maxSpeed = 50f;

    internal Rigidbody body;
    internal Environment environment;

    public override void Initialize()
    {
        base.Initialize();
        environment = GetComponentInParent<Environment>();
        body = GetComponent<Rigidbody>();

        InvokeRepeating(nameof(AddNotOnDestinationReward), 0, 1.0f);
        InvokeRepeating(nameof(AddMovesTooFastReward), 0, 1.0f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Add current position as observation.
        // 3 observations
        sensor.AddObservation(transform.localPosition);

        // Add current speed as observation.
        // 1 observation
        sensor.AddObservation(body.velocity.x);

        // Total = 4 observations
    }

    public override void Heuristic(float[] actionsOut)
    {
        // Check for input.
        if (Input.GetKey(KeyCode.UpArrow))
            actionsOut[0] = 1f;
        else if (Input.GetKey(KeyCode.DownArrow))
            actionsOut[0] = -1f;
        else
            actionsOut[0] = 0f;

    }

    public override void OnActionReceived(float[] vectorAction)
    {
        // Apply movement.
        if (vectorAction[0] == 1)
            body.AddForce(transform.forward * maxSpeed, ForceMode.Acceleration);
        else if (vectorAction[0] == -1)
            body.AddForce(-0.8f * body.velocity);
    }

    public abstract void OnCollisionEnter(Collision other);

    /// <summary>
    /// Update environment variable based on the current parent.
    /// </summary>
    internal void UpdateEnvironment()
    {
        if (environment == null) environment = GetComponentInParent<Environment>();
    }

    /// <summary>
    /// Reward logic for not being on location yet.
    /// </summary>
    internal abstract void AddNotOnDestinationReward();

    /// <summary>
    /// Reward logic for moving too fast.
    /// </summary>
    internal abstract void AddMovesTooFastReward();
}
