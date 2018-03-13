using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeTrigger : MonoBehaviour 
{
	public BikeManager bikeManager;

    void OnTriggerEnter(Collider other) 
	{
		bikeManager.Die();
    }
}
