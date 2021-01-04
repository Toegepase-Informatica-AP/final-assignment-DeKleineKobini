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
        float randomTime = Random.Range(MIN_TIME_START, MAX_TIME_START);
        
        Invoke(nameof(Spawn), randomTime);
    }

    public void Spawn()
    {
        if (environment == null)
        {
            environment = GetComponentInParent<Environment>();
        }
        
        int randomNumber = Random.Range(0, 3);
        
        float randomTime = Random.Range(MIN_TIME, MAX_TIME);

        GameObject prefab;

        if (randomNumber == 0)
        {
            prefab = environment.badCar.gameObject;
        }
        else
        {
            prefab = environment.goodCar.gameObject;
        }
        
        Quaternion orientation = Quaternion.Euler(0, 90, 0);;
        if (roadSide == RoadSide.Left)
        {
            orientation = Quaternion.Euler(0, 270, 0);
        }
        
        GameObject car = Instantiate(prefab, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), orientation);

        car.transform.SetParent(environment.cars.transform, false);
        
        Invoke(nameof(Spawn), randomTime);
    }
}