using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Simple script to call respawn once any collider enters the bike driver trigger
 */
public class BikeTrigger : MonoBehaviour 
{
	public BikeManager bikeManager;

    void OnTriggerEnter(Collider other) 
	{
		if (other.isTrigger == false)
			bikeManager.Die();
    }
}
