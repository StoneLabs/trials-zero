using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script that holds reference to all important parts of the bike.
 * It also takes thrust and balance input and sets the engines accordingly.
 */
public class BikeManager : MonoBehaviour
{
	//Bike part references
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
	// Random impulse applied on death to increase the
	// chance of falling along the z axis.
	public Vector2 impulseRange;

	// Current bike state
	private float thrust = 0.0f;
	private float balance = 0.0f;	
	private bool reverse = false;
	private bool alive = true;
	public bool isAlive() {return alive;}

	void Start () { }

	private void setBikeState(float balance, float thrust, bool reverse)
	{
		// Apply thrust and balance to all engines
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
			// Apply thrust and balance to all engines
			setBikeState(this.balance, this.thrust, this.reverse);
		}
	}

	// Public function to kill the driver
	public void Die()
	{
		if (!this.alive) return;
		DisableDriver();

		// Change the driver color to red
		foreach (MeshRenderer renderer in riderParts)
		{
			renderer.materials[0].color = Color.red;
		}

	}

	// Public function to distable all driver input and impose a random force parallel to the normal of the bikes plane
	public void DisableDriver()
	{
		if (!this.alive) return;
		this.alive = false;

		//Apply impulse in range
		float impulseScale = Random.Range(impulseRange.x, impulseRange.y);
		setBikeState(0, 0, false);
		foreach (Rigidbody rigidbody in rigidbodies)
		{	
			rigidbody.constraints = RigidbodyConstraints.None;
			rigidbody.AddForce(Vector3.forward * impulseScale, ForceMode.Impulse);
		}
	}

	// Public function to set current vike thrust (in range -1 to 1)
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
	// Public function to set balance (in range -1 to 1)
	public void SetBalance(float bal) { this.balance = bal; }
}
