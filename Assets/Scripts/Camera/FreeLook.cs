using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// A simple free camera to be added to a Unity game object.
// 
// Keys:
//	wasd / arrows	- movement
//	q/e 			- up/down (local space)
//	r/f 			- up/down (world space)
//	pageup/pagedown	- up/down (world space)
//	hold shift		- enable fast movement mode
//	right mouse  	- enable free look
//	mouse			- free look / rotation
public class FreeLook : MonoBehaviour
{
    // Normal speed of camera movement.
	[LabelOverride("Movement Speed")]
    public float movementSpeed = 10f;

    // Speed of camera movement when shift is held down,
	[LabelOverride("Movement Speed (Shift)")]
    public float fastMovementSpeed = 100f;

    // Sensitivity for free look. (X-Axis)
	[LabelOverride("Sensitivity X-Axis")]
    public float freeLookSensitivityX = 3f;

    // Sensitivity for free look.
	[LabelOverride("Sensitivity Y-Axis")]
    public float freeLookSensitivityY = 3f;

	[LabelOverride("Camera rotation range (Horizontal)")]
    public Vector2 yrange;
    [Space(13)]
    
	//[LabelOverride("Camera rotation range (Y)")]
    //public Vector2 yrange;
    //[Space(15)]

    // Amount to zoom the camera when using the mouse wheel.
	[LabelOverride("Sensitivity Zoom")]
    public float zoomSensitivity = 10f;

    // Amount to zoom the camera when using the mouse wheel (fast mode).
	[LabelOverride("Sensitivity Zoom (Shift)")]
    public float fastZoomSensitivity = 50f;

    // Set to true when free looking (on right mouse button).
    private bool looking = false;

    void Update()
    {
        var fastMode = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        var movementSpeed = fastMode ? this.fastMovementSpeed : this.movementSpeed;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = transform.position + (-transform.right * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = transform.position + (transform.right * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = transform.position + (transform.forward * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = transform.position + (-transform.forward * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.position = transform.position + (transform.up * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.position = transform.position + (-transform.up * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.PageUp))
        {
            transform.position = transform.position + (Vector3.up * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.PageDown))
        {
            transform.position = transform.position + (-Vector3.up * movementSpeed * Time.deltaTime);
        }

        if (looking)
        {
            float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * freeLookSensitivityX;
            float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * freeLookSensitivityY;
            newRotationY = ClampAngle(newRotationY, yrange.x, yrange.y);
            transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
        }

        float axis = Input.GetAxis("Mouse ScrollWheel");
        if (axis != 0)
        {
            var zoomSensitivity = fastMode ? this.fastZoomSensitivity : this.zoomSensitivity;
            transform.position = transform.position + transform.forward * axis * zoomSensitivity;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartLooking();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            StopLooking();
        }
    }

    // accepts e.g. -80, 80
    float ClampAngle(float angle, float from, float to)
    {
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360+from);
        return Mathf.Min(angle, to);
    }

    // Enable free looking.
    public void StartLooking()
    {
        looking = true;
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Disable free looking.
    public void StopLooking()
    {
        looking = false;
        Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
    }

    void OnDisable()
    {
        StopLooking();
    }
}