using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script that holds references to the most important objects in the scene
 */
public class SceneManager : MonoBehaviour 
{
	[Header("Bike related")]

	// A prefab of the bike
	[LabelOverride("Bike Prefab")]
	public Transform prefab;
	// References to the bike and the current spawnpoint set by editor or a CheckpointManager
	[LabelOverride("Bike instance")]
	public Transform bike = null;
	[LabelOverride("Bike Manager")]
	public BikeManager bikeManager = null;

	[Header("Game logic related")]

	[LabelOverride("Spawn Point")]
	public Transform spawnPoint;
	[LabelOverride("Statistics Manager")]
	public StatisticsManager statsticsManager;
	// Wether the player can currently respawn
	private bool canRespawn = true;

	void Start()
	{
		statsticsManager.HandleStart();
	}

	public void Respawn()
	{
		if (!canRespawn) return; 

		// Create a new bike at the spawn point looking in positive x direction. 
		Transform newBike = Instantiate(prefab, spawnPoint.transform.position, spawnPoint.transform.rotation, this.transform) as Transform;
		Transform oldBike = bike;
		
		// Change references
		bike = newBike;
		bikeManager = newBike.GetComponent<BikeManager>();

		// Destroy the old bike after the references are set to prevent nullpointer exceptions
		Destroy(oldBike.gameObject);

		statsticsManager.HandleRespawn();
	}

	public void Finish()
	{
		Debug.Log("FINISHED");
		canRespawn = false;
		bikeManager.DisableDriver();
		statsticsManager.HandleFinish();
	}
}
