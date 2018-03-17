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

	public void Start()
	{
		if (sceneManager == null)
			sceneManager = GameObject.Find("/SceneManager")
									 .GetComponent<SceneManager>();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.attachedRigidbody != null)
		{
			// Only trigger if caused by the bike
			if (other.transform.IsChildOf(sceneManager.bike))
			{
				redLight.enabled = false;
				greenLight.enabled = true;
				sceneManager.spawnPoint = spawnPoint;
			}
		}
	}
}
