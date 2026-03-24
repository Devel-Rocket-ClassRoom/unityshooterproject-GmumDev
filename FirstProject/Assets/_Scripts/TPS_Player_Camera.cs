using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static UnityEngine.InputSystem.InputAction;

public class TPS_Player_Camera : MonoBehaviour
{
	[Header("Player")]
	public Transform playerTransform;

	[Header("Camera")]
	public Transform cameraTransform;
	public Vector3 defaultOffsetToPlayer = new Vector3(0, 2f, -4f);
	public Vector3 zoomOffsetToPlayer = new Vector3(0, 1f, -2f);

	[Header("Camera Pitch Rotation")]
	public float maxPitch = 90f;
	public float minPitch = -90f;
	public Transform[] pitchChildren;

	
	[Header("Camera Yaw Rotation")]
	public float maxYaw = 360f;
	public float minYaw = -360f;
	public Transform[] yawChildren;


	private Vector2 deltaYawPitch = new Vector2();
	private float curRotationX = 0f; // camera-pitch
	private float curRotationY = 0f; // camera-yaw

	[Header("Mouse Settings")]
	public float mouseSens = 100f;

	[Header("Pad Settings")]
	private Vector2[] lookdirs = {Vector2.up, Vector2.down, Vector2.left, Vector2.right};
	public float padSens = 100f;

	// -------Actions
	private InputAction lookAction;
	private InputAction[] lookPadAction = new InputAction[4];
	private InputAction zoomAction;

	// -------Zoom
	public float zoomDuration = 0.4f;
	private bool isZoom = false;
	private float curZoom = 0f;
	private float zoomRange;
	private float zoomSpeed;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		lookAction = InputSystem.actions.FindAction("Look");
		zoomAction = InputSystem.actions.FindAction("Zoom");
		string[] tmp = { "LookUp", "LookDown", "LookLeft", "LookRight" };
		for(int i = 0; i < 4; i++)
		{
			lookPadAction[i] = InputSystem.actions.FindAction(tmp[i]);
		}

		Cursor.lockState = CursorLockMode.Locked;

		lookAction.performed += (x) =>
		{
			deltaYawPitch = GetMouseAxis_Old();// lookAction.ReadValue<Vector2>();
		};
		zoomAction.performed += OnZoom;
		zoomAction.canceled += OffZoom;

		zoomRange = Vector3.Distance(defaultOffsetToPlayer, zoomOffsetToPlayer);
		zoomSpeed = zoomRange/zoomDuration;
		
	}
	void OnZoom(InputAction.CallbackContext action)
	{
		if (action.interaction is HoldInteraction)
		{
			isZoom = true;
			
		}
	}
	void OffZoom(InputAction.CallbackContext action)
	{
		if (action.interaction is HoldInteraction)
		{
			isZoom = false;
		}
	}
	Vector2 GetMouseAxis_Old()
	{
		float h = Input.GetAxis("Mouse X");
		float v = Input.GetAxis("Mouse Y");

		return new Vector2(h, v);	// hor delta for x-axis(pitch), ver delta for y-axis(yaw)
	}
	// Update is called once per frame
	void Update()
	{
		for (int i = 0; i < 4; i++)
		{
			if(lookPadAction[i] != null && lookPadAction[i].IsPressed())
			{
				deltaYawPitch += lookdirs[i];
			}
		}
		//YawAndPitch(deltaYawPitch.normalized * mouseSens * Time.deltaTime);
		YawAndPitch(deltaYawPitch.normalized * padSens * Time.deltaTime);
		deltaYawPitch = Vector2.zero;

		if (isZoom)
		{
			curZoom += zoomSpeed * Time.deltaTime;
		}
		else
		{
			curZoom -= zoomSpeed * Time.deltaTime;
		}
		curZoom = Mathf.Clamp(curZoom, 0, 1);
		cameraTransform.localPosition = Vector3.Slerp(defaultOffsetToPlayer, zoomOffsetToPlayer, curZoom);
	}

	void YawAndPitch_New(Vector2 deltaAxis)
	{
		foreach (var t in yawChildren)
		{
			t.Rotate(Vector3.up * deltaAxis.x);
		}

		foreach (var t in pitchChildren)
		{
			t.Rotate(Vector3.left * deltaAxis.y);
		}
	}
	void YawAndPitch(Vector2 deltaPos)
	{

		// yaw: y-axis
		curRotationY += deltaPos.x;
		curRotationY = Mathf.Clamp(curRotationY, minYaw, maxYaw);
		foreach (var t in yawChildren)
		{
			t.localRotation = Quaternion.Euler(t.localEulerAngles.x, curRotationY, 0);
		}

		// pitch: x-axis
		curRotationX -= deltaPos.y;
		curRotationX = Mathf.Clamp(curRotationX, minPitch, maxPitch);
		foreach (var t in pitchChildren)
		{
			t.localRotation = Quaternion.Euler(curRotationX, t.localEulerAngles.y, 0);
		}
	}
}
