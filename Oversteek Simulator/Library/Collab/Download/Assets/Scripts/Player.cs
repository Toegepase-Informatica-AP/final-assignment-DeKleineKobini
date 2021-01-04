using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.MLAgents;
using UnityEngine;

public class Player : Agent
{
    public float movementSpeed = 1;
    public float rotationSpeed = 300;

    private Environment environment;
    private GameObject playerController;
    private CharacterController controller;

    public override void Initialize()
    {
        base.Initialize();
        environment = GetComponentInParent<Environment>();
        playerController = environment.transform.FindObjectsWithTag("PlayerController").First();
        controller = playerController.GetComponent<CharacterController>();
    }

    public override void OnEpisodeBegin()
    {
        SetReward(0);
        environment.ResetEnvironment();
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
        if (vectorAction[0] != 0)
        {
            controller.Move(playerController.transform.forward * movementSpeed * Time.deltaTime);
        }

        if (vectorAction[1] != 0)
        {
            AddReward(-0.0005f);
            playerController.transform.Rotate(0, rotationSpeed * (vectorAction[1] * 2 - 3) * Time.deltaTime, 0);
        }
    }

    public void OnChildTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Finish"))
        {
            AddReward(1);
            EndEpisode();
        }
    }

    public void OnChildTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Grass"))
        {
            AddReward(-0.0001f);
        }
        else if (collision.CompareTag("Road"))
        {
            AddReward(-0.0001f);
        }
    }

    public void OnChildCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Car"))
        {
            AddReward(-1);
            EndEpisode();
        }
    }
}
