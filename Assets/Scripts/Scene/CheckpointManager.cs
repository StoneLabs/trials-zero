using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour 
{
	public SceneManager sceneManager;
	public Light redLight;
	public Light greenLight;
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
			if (other.transform.IsChildOf(sceneManager.bike))
			{
				redLight.enabled = false;
				greenLight.enabled = true;
				sceneManager.spawnPoint = spawnPoint;
			}
		}
	}
}
