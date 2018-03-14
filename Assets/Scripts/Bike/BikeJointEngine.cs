using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeJointEngine : BikeEngine
{
	[LabelOverride("Connected Joint")]
	public ConfigurableJoint joint;

	[Header("Engine Details")]
	[LabelOverride("Maximal velocity")]
    public AnimationCurve velocityCurve = AnimationCurve.Linear(-1, 0, 1, 0);
	[LabelOverride("Thrust")]
    public AnimationCurve thrustCurve = AnimationCurve.Linear(-1, 0, 1, 0);

	private JointDrive angularXDrive = new JointDrive();

	public void Start()
	{
		angularXDrive.maximumForce = 3.402823e+38f;
	}

	public override void SetEngineState(float thrust, bool reverse)
	{
		float thrustSign = (reverse?-1:1);
		joint.targetAngularVelocity = new Vector3(velocityCurve.Evaluate(thrust * thrustSign) * (inverse?-1:1) * thrustSign,0,0);
		angularXDrive.positionDamper = thrustCurve.Evaluate(thrust * thrustSign);

		joint.angularXDrive = angularXDrive;
	}
}
