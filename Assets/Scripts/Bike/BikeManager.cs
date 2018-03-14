using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeManager : MonoBehaviour
{
	[Header("Bike Parts")]

	[LabelOverride("Bike Body")]
	public BikeBody body;
	
	[SerializeField]
	[LabelOverride("Bike Engines")]
	public BikeEngine[] engines;

	[LabelOverride("Rigidbodies")]
	public Rigidbody[] rigidbodies;

	[LabelOverride("Rider Mesh")]
	public MeshRenderer[] riderParts;

	[Header("Bike Behaviour")]
	public Vector2 impulseRange;

	private float thrust = 0.0f;
	private float balance = 0.0f;	
	private bool reverse = false;
	private bool alive = true;

	void Start () { }

	private void setBikeState(float balance, float thrust, bool reverse)
	{
		body.SetBalance(balance);
		body.SetEngineState(thrust, reverse);
		foreach (BikeEngine engine in engines) 
		{
			engine.SetEngineState(thrust, reverse);
		}
	}

	void FixedUpdate()
	{
		if (alive)
		{
			setBikeState(this.balance, this.thrust, this.reverse);
		}
	}

	public void Die()
	{
		this.alive = false;
			
		foreach (MeshRenderer renderer in riderParts)
		{
			renderer.materials[0].color = Color.red;
		}

		float impulseScale = Random.Range(impulseRange.x, impulseRange.y);
		setBikeState(0, 0, false);
		foreach (Rigidbody rigidbody in rigidbodies)
		{	
			rigidbody.constraints = RigidbodyConstraints.None;
			rigidbody.AddForce(Vector3.forward * impulseScale, ForceMode.Impulse);
		}
	}

	public void SetThrust(float thr) 
	{
		if (thr >= 0 && thr <= 1)
		{
			this.reverse = false;
			this.thrust = thr; 
		}
		else if (thr >= -1 && thr < 0)
		{
			this.reverse = true;
			this.thrust = -thr; 
		}
	}
	public void SetBalance(float bal) { this.balance = bal; }
}
