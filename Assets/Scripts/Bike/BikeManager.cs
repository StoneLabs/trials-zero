using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeManager : MonoBehaviour
{
	public float thrust = 0.0f;
	public float balance = 0.0f;	
	public bool reverse = false;
	public bool alive = true;

	[SerializeField]
	public BikeEngine[] engines;
	public BikeBody body;

	public Rigidbody[] rigidbodies;

	public MeshRenderer[] riderParts;

	void Start () { }

	private void setBikeState(float balance, float thrust, bool reverse)
	{
		body.SetBalance(balance);
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

		setBikeState(0, 0, false);
		foreach (Rigidbody rigidbody in rigidbodies)
			rigidbody.constraints = RigidbodyConstraints.None;
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
