using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowX : MonoBehaviour 
{
	public SceneManager sceneManager;

	public void Start()
	{
		if (sceneManager == null)
			sceneManager = GameObject.Find("/SceneManager")
									 .GetComponent<SceneManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.position = new Vector3(sceneManager.bikeManager.body.transform.position.x,
											  sceneManager.bikeManager.body.transform.position.y + 1.5f,
											  this.transform.position.z);
	}
}
