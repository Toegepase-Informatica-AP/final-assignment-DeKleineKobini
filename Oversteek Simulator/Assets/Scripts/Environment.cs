using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public enum RoadSide
{
    Right,
    Left
}
public class Environment : MonoBehaviour
{
    private const float SPAWN_RANGE_X = 20f;
    private const float SPAWN_RANGE_Z = 14f;
    
    public int maxSpeed = 30;
    public GameObject goodCar;
    public GameObject badCar;
    public GameObject scoreboard;

    private Player player;
    private GameObject playerController;
    private CharacterController c;
    private TextMeshPro _scoreboard;
    private Vector3 initialControllerPosition;
    private Quaternion initialControllerRotation;
    
    internal GameObject cars;


    public void OnEnable()
    {
        player = GetComponentInChildren<Player>();
        playerController = player.transform.FindObjectsWithTag("PlayerController").First();
        c = playerController.GetComponent<CharacterController>();
        _scoreboard = scoreboard.GetComponent<TextMeshPro>();
        initialControllerPosition = playerController.transform.localPosition;
        initialControllerRotation = playerController.transform.localRotation;
        
        cars = transform.Find("Cars").gameObject;
    }

    private void FixedUpdate()
    {
        _scoreboard.text = player.GetCumulativeReward().ToString("f3");
    }

    // bron: https://github.com/ddhaese/Project_ML-Agents_02/blob/master/Assets/Scripts/Environment.cs, lijn 31 - 37
    private Vector3 GetRandomPosition()
    {
        int randomNumber = Random.Range(0, 3);
        
        float x = Random.Range(-SPAWN_RANGE_X, SPAWN_RANGE_X);
        float z = Random.Range(initialControllerPosition.z, SPAWN_RANGE_Z);
        
        if (randomNumber == 0)
        {
            return new Vector3(initialControllerPosition.x, 1f, z);
        }
        
        return new Vector3(x, 1f, initialControllerPosition.z);
    }

    private Quaternion GetRandomRotation()
    {
        // bron: https://github.com/ddhaese/Project_ML-Agents_02/blob/master/Assets/Scripts/Environment.cs, lijn 77
        return Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
    }

    public void ResetEnvironment()
    {
        foreach (Transform car in cars.transform)
        {
            GameObject.Destroy(car.gameObject);
        }
        
        c.enabled = false;
        c.transform.localPosition = GetRandomPosition();
        c.transform.localRotation = GetRandomRotation();
        c.enabled = true;
    }
}
