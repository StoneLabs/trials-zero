using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Simple script to allow the camera to follow the bike through x and y axis.
 * Just a simple temp. solution. Needs furthor work.
 */
public class FollowX : MonoBehaviour 
{
	//Target bike
	public SceneManager sceneManager;

	public void Start()
	{
		if (sceneManager == null)
			sceneManager = GameObject.Find("/SceneManager")
									 .GetComponent<SceneManager>();
	}
	
	void Update () 
	{
		// Follow bike on x and y axis. Z is set through editor placement.
		this.transform.position = new Vector3(sceneManager.bikeManager.body.transform.position.x,
											  sceneManager.bikeManager.body.transform.position.y + 1.5f,
											  this.transform.position.z);
	}
}
