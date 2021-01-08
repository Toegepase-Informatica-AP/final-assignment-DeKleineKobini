using Assets.Scripts;
using TMPro;
using Unity.MLAgents;
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
    public int spawnY = 0;
    public int maxSpeed = 30;
    public GameObject goodCar;
    public GameObject badCar;
    public GameObject scoreboard;
    public GameObject finish;

    private GameObject player;
    private TextMeshPro _scoreboard;
    private Vector3 initialPlayerPosition;

    internal GameObject cars;

    public void OnEnable()
    {
        player = transform.GetChildrenByTag("Player").gameObject;
        cars = transform.Find("Cars").gameObject;

        if (scoreboard != null) _scoreboard = scoreboard.GetComponent<TextMeshPro>();
        initialPlayerPosition = player.transform.localPosition;

    }

    private void FixedUpdate()
    {
        var agent = player.GetComponent<Agent>();
        if (_scoreboard != null && agent != null) _scoreboard.text = agent.GetCumulativeReward().ToString("f3");
    }


    /* Source:
     * - https://github.com/ddhaese/Project_ML-Agents_02/blob/master/Assets/Scripts/Environment.cs
     * - lines 31 to 37
     */
    /// <summary>
    /// Get a random position for the player spawning.
    /// Spawning on the crossing has a 1 in 3 chance.
    /// </summary>
    private Vector3 GetRandomPosition()
    {
        int randomNumber = Random.Range(0, 3);

        if (randomNumber == 0) // Spawn on crossing.
        {
            float z = Random.Range(initialPlayerPosition.z, SPAWN_RANGE_Z);

            return new Vector3(initialPlayerPosition.x, spawnY, z);
        }

        float x = Random.Range(-SPAWN_RANGE_X, SPAWN_RANGE_X);

        return new Vector3(x, spawnY, initialPlayerPosition.z);
    }

    /* Source:
     * - https://github.com/ddhaese/Project_ML-Agents_02/blob/master/Assets/Scripts/Environment.cs
     * - line 77
     */
    /// <summary>
    /// Get a random rotation for the player.
    /// </summary>
    private Quaternion GetRandomRotation()
    {
        // bron: https://github.com/ddhaese/Project_ML-Agents_02/blob/master/Assets/Scripts/Environment.cs, lijn 77
        return Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
    }

    /// <summary>
    /// Resets the current environment to the initial state.
    /// 
    /// Removes all existing calls. Resets player location and rotation.
    /// </summary>
    public void ResetEnvironment()
    {
        // Loop over all existing cars.
        foreach (Transform car in cars.transform)
        {
            // Destroy the car.
            GameObject.Destroy(car.gameObject);
        }

        // Reset the player's location.
        player.transform.localPosition = GetRandomPosition();
        // Reset the player's rotation.
        player.transform.localRotation = GetRandomRotation();
    }
}
