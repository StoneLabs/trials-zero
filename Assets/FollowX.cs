using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowX : MonoBehaviour 
{
	public SceneManager target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.position = new Vector3(target.bikeManager.body.transform.position.x,
											  target.bikeManager.body.transform.position.y + 1.5f,
											  this.transform.position.z);
	}
}
