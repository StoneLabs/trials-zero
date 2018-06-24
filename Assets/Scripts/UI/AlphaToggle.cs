using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaToggle : MonoBehaviour 
{
	[LabelOverride("Toggle Key")]
	public KeyCode togglekey = KeyCode.Space;
	[LabelOverride("Toggle Target")]
	public GameObject toggletarget;

	void Update () 
	{
		if (Input.GetKeyDown(togglekey))
			toggletarget.SetActive(!toggletarget.active);
	}
}
