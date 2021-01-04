using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadCar : Car
{
    public override void AddNotOnDestinationReward()
    {
        AddReward(-0.002f);
    }
    
    public override void AddMovesTooFastReward()
    {
        if (environment == null)
        {
            environment = GetComponentInParent<Environment>();
        }
        
        if (environment != null && rb.velocity.x > (environment.maxSpeed + 10))
        {
            AddReward(-0.1f);
        }
    }
    
    public override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerController"))
        {
            AddReward(-0.5f);
            Destroy(this.gameObject);
            EndEpisode();
        } else if (other.gameObject.tag.Contains("Destination"))
        {
            AddReward(1f);
            Destroy(this.gameObject);
            EndEpisode();
        } else if (other.gameObject.CompareTag("Car"))
        {
            AddReward(-0.8f);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            EndEpisode();
        }
    }
}
