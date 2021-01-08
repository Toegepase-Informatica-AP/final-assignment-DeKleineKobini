using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPoint : MonoBehaviour
{
    private const float MIN_TIME_START = 0f;
    private const float MAX_TIME_START = 5f;
    private const float MIN_TIME = 2f;
    private const float MAX_TIME = 20f;

    public RoadSide roadSide;

    private Environment environment;

    private void Start()
    {
        environment = GetComponentInParent<Environment>();

        var randomTime = Random.Range(MIN_TIME_START, MAX_TIME_START);
        Invoke(nameof(Spawn), randomTime);
    }

    /// <summary>
    /// Spawn a new car. Self-invoking method.
    /// </summary>
    public void Spawn()
    {
        if (environment == null) environment = GetComponentInParent<Environment>();

        // Decide which car to spawn
        int randomNumber = Random.Range(0, 3);
        var prefab = randomNumber == 0 ? environment.badCar.gameObject : environment.goodCar.gameObject;
        // Set the right orientation.
        var orientation = Quaternion.Euler(0, roadSide == RoadSide.Left ? 270 : 90, 0);
        // Get the location of the spawn point.
        var location = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);

        // Create the new car.
        var car = Instantiate(prefab, location, orientation);
        car.transform.SetParent(environment.cars.transform, false);

        // Actually spawn the car.
        var randomTime = Random.Range(MIN_TIME, MAX_TIME);
        Invoke(nameof(Spawn), randomTime);
    }
}