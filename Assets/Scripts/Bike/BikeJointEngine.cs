using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* An engine using configurable joints 
 */
public class BikeJointEngine : BikeEngine
{
	[LabelOverride("Connected Joint")]
	public ConfigurableJoint joint;

	[Header("Engine Details")]
	[LabelOverride("Invert Joint Rotation")]
	public bool inverse = true;
	[LabelOverride("Maximal velocity")]
    public AnimationCurve velocityCurve = AnimationCurve.Linear(-1, 0, 1, 0);
	[LabelOverride("Thrust")]
    public AnimationCurve thrustCurve = AnimationCurve.Linear(-1, 0, 1, 0);

	// Empty JointDrive used to set the one in the connected joint 
	private JointDrive angularXDrive = new JointDrive();

	public void Start()
	{
		angularXDrive.maximumForce = 3.402823e+38f;
	}

	public override void SetEngineState(float thrust, bool reverse)
	{
		float directionalThrust = thrust * (reverse ? -1 : 1);
		reverse = reverse ^ inverse; // Apply engine inverse

		// Set joint drive target velocity based on thrust and the velocity curve
		// and apply it depending on reverse
		joint.targetAngularVelocity = new Vector3(velocityCurve.Evaluate(directionalThrust) * (reverse?-1:1),0,0);
		angularXDrive.positionDamper = thrustCurve.Evaluate(directionalThrust);

		// Set joint engine state
		joint.angularXDrive = angularXDrive;
	}
}
