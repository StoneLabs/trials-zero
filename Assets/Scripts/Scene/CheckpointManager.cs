using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Simple class to manage checkpoint behaviour.
 */
public class CheckpointManager : MonoBehaviour 
{
	public SceneManager sceneManager;

	// The light components of the checkpoint
	public Light redLight;
	public Light greenLight;

	// The transform that will be pass to the scene manager as a spawn point
	public Transform spawnPoint;

	// Wether this checkpoint functions as a finish line
	public bool isFinishLine = false;

	// Will become true once the driver enters the trigger
	private bool triggered = false;

	public void Start()
	{
		if (sceneManager == null)
			sceneManager = GameObject.Find("/SceneManager")
									 .GetComponent<SceneManager>();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (triggered) return; // Dont finish twice
		if (other.attachedRigidbody != null)
		{
			// Only trigger if caused by the bike
			if (other.transform.IsChildOf(sceneManager.bike))
			{
				triggered = true;
				redLight.enabled = false;
				greenLight.enabled = true;
				if (isFinishLine)
					sceneManager.Finish();
				else
					sceneManager.spawnPoint = spawnPoint;
			}
		}
	}
}
