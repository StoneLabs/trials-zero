using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script that holds references to the most important objects in the scene
 */
public class SceneManager : MonoBehaviour 
{
	// A prefab of the bike
	public Transform prefab;

	// References to the bike and the current spawnpoint set by editor or a CheckpointManager
	public Transform bike = null;
	public BikeManager bikeManager = null;
	public Transform spawnPoint;

	public void Respawn()
	{
		// Create a new bike at the spawn point looking in positive x direction. 
		Transform newBike = Instantiate(prefab, spawnPoint.transform.position, spawnPoint.transform.rotation, this.transform) as Transform;
		Transform oldBike = bike;
		
		// Change references
		bike = newBike;
		bikeManager = newBike.GetComponent<BikeManager>();

		// Destroy the old bike after the references are set to prevent nullpointer exceptions
		Destroy(oldBike.gameObject);
	}
}
