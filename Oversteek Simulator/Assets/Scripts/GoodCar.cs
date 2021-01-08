using UnityEngine;

public class GoodCar : Car
{
    public override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerController"))
        {
            AddReward(-1f);
            Destroy(this.gameObject);
            EndEpisode();
        }
        else if (other.gameObject.tag.Contains("Destination"))
        {
            AddReward(1f);
            Destroy(this.gameObject);
            EndEpisode();
        }
        else if (other.gameObject.CompareTag("Car"))
        {
            AddReward(-0.8f);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            EndEpisode();
        }
    }

    internal override void AddNotOnDestinationReward()
    {
        AddReward(-0.001f);
    }

    internal override void AddMovesTooFastReward()
    {
        UpdateEnvironment();

        if (environment != null && body.velocity.x > environment.maxSpeed)
        {
            AddReward(-0.1f);
        }
    }
}
