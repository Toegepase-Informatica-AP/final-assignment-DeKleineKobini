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
    private const float SPAWN_RANGE_X = 45;
    private const float SPAWN_RANGE_Z = 6;

    public int spawnX = 0;
    public int maxSpeed = 30;
    public GameObject goodCar;
    public GameObject badCar;
    public GameObject scoreboard;
    public GameObject finish;

    private Player player;
    // private GameObject playerObject;
    private TextMeshPro _scoreboard;
    private Vector3 initialPlayerPosition;
    
    internal GameObject cars;

    public void OnEnable()
    {
        player = GetComponentInChildren<Player>();
        // playerObject = player.transform.FindObjectsWithTag("PlayerController").First();
        _scoreboard = scoreboard.GetComponent<TextMeshPro>();
        initialPlayerPosition = player.transform.localPosition;
        
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
        
        if (randomNumber == 0) // Spawn on crossing.
        {
            float z = Random.Range(initialPlayerPosition.z, SPAWN_RANGE_Z);

            return new Vector3(initialPlayerPosition.x, 0, z);
        }

        float x = Random.Range(-SPAWN_RANGE_X, SPAWN_RANGE_X);

        return new Vector3(x, 0, initialPlayerPosition.z);
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
        
        player.transform.localPosition = GetRandomPosition();
        player.transform.localRotation = GetRandomRotation();
    }
}
