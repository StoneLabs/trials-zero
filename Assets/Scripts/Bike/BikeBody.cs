using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BikeBody : MonoBehaviour
{
	public ConstantForce constantForce;

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
		constantForce.relativeTorque = new Vector3(force_balance + force_rotation, 0, 0);
	}
	


	public void SetBalance(float balance)
	{
		force_balance = balanceForceCurve.Evaluate(Mathf.Abs(balance)) * Mathf.Sign(balance) * (balanceInverse?-1:1);
	}


	public void SetEngineState(float thrust, bool reverse)
	{
		force_rotation = rotationForceCurve.Evaluate(thrust * (reverse?-1:1)) * (rotationInverse?-1:1) * (reverse?-1:1);
	}
}
