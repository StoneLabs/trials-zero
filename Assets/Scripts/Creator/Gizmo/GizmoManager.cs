using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GizmoManager : MonoBehaviour 
{
	public enum GizmoMode {Translate, Rotate, Scale}

	[Header("Settings")]
	[LabelOverride("Mode UI label")]
	public Text label_mode;

	[LabelOverride("Target UI label")]
	public Text label_target;
	
	[LabelOverride("Mode swap key")]
	public KeyCode swapkey_mode = KeyCode.Space;
	
	[LabelOverride("Editable layer")]
	public LayerMask layerMask_editable = -1;


	[Header("UI Texts")]
	[LabelOverride("Translaation mode")]
	public string label_mode_translation = "TRANSLATE";
	
	[LabelOverride("Rotation mode")]
	public string label_mode_rotation = "ROTATE";

	[LabelOverride("Scale mode")]
	public string label_mode_scale = "SCALE";


	[Header("Gizmo Objects")]
	[LabelOverride("Transformation object")]
	public Transform gizmoTranslate;

	[LabelOverride("Transformation script")]
	public GizmoTranslateScript gizmoTranslateScript;

	[Space(5)]
	[LabelOverride("Rotation object")]
	public Transform gizmoRotation;

	[LabelOverride("Rotation script")]
	public GizmoRotateScript gizmoRotationScript;

	[Space(5)]
	[LabelOverride("Scale object")]
	public Transform gizmoScale;
	
	[LabelOverride("Scale script")]
	public GizmoScaleScript gizmoScaleScript;


	[Header("Current Settings")]
	[LabelOverride("Current target object")]
	public Transform target = null;

	[LabelOverride("Current gizmo mode")]
	public GizmoMode mode;

	void Update()
	{
		if (Input.GetKeyDown(swapkey_mode))
		{
			mode = NextGizmo(mode);
			SetGizmo();
		}	     

		if(Input.GetMouseButtonDown(0))
     	{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if( Physics.Raycast(ray, out hit, 100, layerMask_editable.value))
				SetGizmo(hit.transform);
		}
	}

	public void SetGizmo(Transform newTarget = null)
	{
		target = newTarget ?? target;

		if (target != null)
		{
			gizmoTranslateScript.translateTarget = target.gameObject;
			gizmoTranslateScript.Awake();

			gizmoRotationScript.rotateTarget = target.gameObject;
			gizmoRotationScript.Awake();
			
			gizmoScaleScript.scaleTarget = target.gameObject;
			gizmoScaleScript.Awake();
		}

		gizmoTranslate.gameObject.SetActive(target != null && mode == GizmoMode.Translate);
		gizmoRotation.gameObject.SetActive(target != null && mode == GizmoMode.Rotate);
		gizmoScale.gameObject.SetActive(target != null && mode == GizmoMode.Scale);

		label_mode.text = mode == GizmoMode.Translate ? label_mode_translation
						: mode == GizmoMode.Rotate ? label_mode_rotation
						: label_mode_scale;
		label_target.text = target != null ? target.gameObject.name : "NONE";
	}

	public static GizmoMode NextGizmo(GizmoMode value)
    {
        return (from GizmoMode val in Enum.GetValues(typeof (GizmoMode)) 
                where val > value 
                orderby val 
                select val).DefaultIfEmpty().First();
    }
}
