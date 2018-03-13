using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeEngine : MonoBehaviour
{
	public ConfigurableJoint joint;
	public bool inverse = true;

	public float targetVelocity;
	public float thrustLimit;

	JointDrive angularXDrive = new JointDrive();

	public void Start()
	{
		angularXDrive.maximumForce = 3.402823e+38f;
	}

	public void SetEngineState(float thrust, bool reverse)
	{
		joint.targetAngularVelocity = new Vector3(targetVelocity * (inverse?-1:1) * (reverse?-1:1),0,0);

		angularXDrive.positionDamper = thrust * thrustLimit;
		joint.angularXDrive = angularXDrive;
	}
}
