using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour 
{
	public Transform prefab;
	public Transform bike = null;
	public BikeManager bikeManager = null;
	public Transform spawnPoint;

	public void Respawn()
	{
		Transform newBike = Instantiate(prefab, spawnPoint.transform.position, spawnPoint.transform.rotation, this.transform) as Transform;
		Transform oldBike = bike;
		
		bike = newBike;
		bikeManager = newBike.GetComponent<BikeManager>();
		Destroy(oldBike.gameObject);
	}
}
