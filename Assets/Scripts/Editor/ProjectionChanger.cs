using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectionChanger : MonoBehaviour
{
	[LabelOverride("Camera")]
	public Camera camera;

	[LabelOverride("Key")]
	public KeyCode swapkey = KeyCode.M;

	[Header("UI Elements")]

	[LabelOverride("UI Label")]
	public Text label;

	[LabelOverride("Label Text (Perspective)")]
	public string text_pers = "";

	[LabelOverride("Label Text (Orthographic)")]
	public string text_orth = "";

	private bool perspective = false;
	
	void Update()
	{
		if (Input.GetKeyDown(swapkey))
		{
			perspective = !perspective;
			label.text = perspective ? text_pers : text_orth;
			camera.orthographic = !perspective;
		}
	}
}
