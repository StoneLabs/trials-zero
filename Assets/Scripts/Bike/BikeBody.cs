using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeBody : MonoBehaviour
{
	public ConstantForce conf;
	public bool inverse = true;

	public float velocity;
	public float target;
	public float current;

	public void SetBalance(float bal)
	{
		target = bal * velocity;
		current = current + (target-current) * Time.deltaTime * 10;

		conf.relativeTorque = new Vector3(current, 0, 0);
	}
}
