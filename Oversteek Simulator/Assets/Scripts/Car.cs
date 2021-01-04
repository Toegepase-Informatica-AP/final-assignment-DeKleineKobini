using System;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public abstract class Car : Agent
{
    public float maxSpeed = 50f;

    internal Rigidbody rb;
    internal Environment environment;

    public override void Initialize()
    {
        base.Initialize();
        rb = GetComponent<Rigidbody>();

        InvokeRepeating(nameof(AddNotOnDestinationReward), 0, 1.0f);
        InvokeRepeating(nameof(AddMovesTooFastReward), 0, 1.0f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition); // 3 observations

        sensor.AddObservation(rb.velocity.x); // 1 observation

        // total: 4 observations
    }

    public abstract void AddNotOnDestinationReward();

    public abstract void AddMovesTooFastReward();

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0f; // don't move

        if (Input.GetKey(KeyCode.UpArrow)) // go forward
        {
            actionsOut[0] = 1f;
        }
        if (Input.GetKey(KeyCode.DownArrow)) // stop
        {
            actionsOut[0] = -1f;
        }
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        if (vectorAction[0] == 1)
        {
            Move();
        }

        if (vectorAction[0] == -1)
        {
            Stop();
        }
    }

    public void Move()
    {
        rb.AddForce(transform.forward * maxSpeed, ForceMode.Acceleration);
    }

    public void Stop()
    {
        rb.AddForce(-0.8f * rb.velocity);
    }

    public abstract void OnCollisionEnter(Collision other);
}
