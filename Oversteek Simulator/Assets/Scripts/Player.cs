using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class Player : Agent
{
    public float movementSpeed = 1;
    public float rotationSpeed = 300;
    private float rewardFactor = 0.001f;

    private Environment environment;
    private CharacterController controller;
    private GameObject finish;
    private Rigidbody rb;
    private bool isMoving = false;

    public override void Initialize()
    {
        base.Initialize();
        environment = GetComponentInParent<Environment>();
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        finish = environment.finish;
    }

    public override void OnEpisodeBegin()
    {
        rb.velocity = new Vector3(0, 0, 0);
        environment.ResetEnvironment();
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.localPosition, finish.transform.localPosition);

        if (distance < 25 && isMoving)
        {
            AddReward(rewardFactor/distance);
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        float distance = Vector3.Distance(transform.localPosition, finish.transform.localPosition);
        
        sensor.AddObservation(distance); // 1 observation
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0f;
        actionsOut[1] = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            actionsOut[0] = 1f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            actionsOut[1] = 1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            actionsOut[1] = 2f;
        }

    }

    public override void OnActionReceived(float[] vectorAction)
    {
        isMoving = false;
        if (vectorAction[0] != 0)
        {
            transform.position += transform.forward * movementSpeed * Time.deltaTime * 2;
            isMoving = true;
        }

        if (vectorAction[1] != 0)
        {
            transform.Rotate(0, rotationSpeed * (vectorAction[1] * 2 - 3) * Time.deltaTime, 0);
        }
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
