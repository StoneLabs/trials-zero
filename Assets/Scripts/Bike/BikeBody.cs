using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/* Class to manage balance effects and
 * to counter act the effects of the wheel rotation
 */
public class BikeBody : MonoBehaviour
{
	public ConstantForce constantForce;

	// Variables to store balance and counter rotation force
	private float force_balance;
	private float force_rotation;

	[Header("Balance Machine")]
	public bool balanceInverse = false;
    public AnimationCurve balanceForceCurve = AnimationCurve.Linear(0, 0, 1, 450);
	
	[Header("Counter Rotation Machine")]
	public bool rotationInverse = false;
    public AnimationCurve rotationForceCurve = AnimationCurve.Linear(-1, 0, 1, 0);

	void FixedUpdate()
	{
		// Set constant force to the sum of individual forces
		constantForce.relativeTorque = new Vector3(force_balance + force_rotation, 0, 0);
	}
	
	// Set balance and calculate the equivalent force
	public void SetBalance(float balance)
	{
		force_balance = balanceForceCurve.Evaluate(Mathf.Abs(balance)) * Mathf.Sign(balance) * (balanceInverse?-1:1);
	}

	// Set counter rotation force according to current thrust
	public void SetEngineState(float thrust, bool reverse)
	{
		force_rotation = rotationForceCurve.Evaluate(thrust * (reverse?-1:1)) * (rotationInverse?-1:1) * (reverse?-1:1);
	}
}
