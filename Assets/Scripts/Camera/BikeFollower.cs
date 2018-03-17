using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Simple script to allow the camera to follow the bike through x and y axis.
 * Just a simple temp. solution. Needs furthor work.
 */
public class BikeFollower : MonoBehaviour 
{
	//Target bike
	public SceneManager sceneManager;

	[Header("Offset")]
	public float distanceX;
	public float distanceY;
	public float distanceZ;

	public void Start()
	{
		if (sceneManager == null)
			sceneManager = GameObject.Find("/SceneManager")
									 .GetComponent<SceneManager>();
	}
	
	void Update () 
	{
		// Follow bike on all axis.
		this.transform.position = new Vector3(sceneManager.bikeManager.body.transform.position.x + distanceX,
											  sceneManager.bikeManager.body.transform.position.y + distanceY,
											  sceneManager.bikeManager.body.transform.position.z + distanceZ);
	}
}
