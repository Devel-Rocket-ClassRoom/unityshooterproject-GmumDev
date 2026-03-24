using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public float speed = 10f;
	public float jumpSpd = 50f;

	InputAction moveAction;
	InputAction jumpAction;

	Rigidbody rb;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
		jumpAction = InputSystem.actions.FindAction("Jump");

		rb = GetComponent<Rigidbody>();

		jumpAction.started += (InputAction.CallbackContext callback) =>
		{
			rb.AddForce(Vector3.up * jumpSpd, ForceMode.Impulse);
		};

	}

    // Update is called once per frame
    void Update()
    {
		//Move_With_OldSystem();

		Move_With_InputSystem();
	}
	public void Update_Move()
	{
		Move_With_InputSystem();
	}
	private void Move_With_InputSystem()
	{
		Vector2 v = moveAction.ReadValue<Vector2>();

		Vector3 dir = transform.forward * v.y + transform.right * v.x;
		dir.y = 0;
		transform.position += dir.normalized * speed * Time.deltaTime;
	}

	private void Move_With_OldSystem()
	{
        if(Input.GetKey(KeyCode.W))
        {
			transform.Translate(0, 0, speed * Time.deltaTime);
			//Debug.Log("W Pressed");
			//transform.position += transform.forward * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.Translate(0, 0, -speed * Time.deltaTime);
			//transform.position -= transform.forward * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.Translate(speed * Time.deltaTime, 0, 0);
			//transform.position += transform.right * speed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.Translate(-speed * Time.deltaTime, 0, 0);
			//transform.position -= transform.right * speed * Time.deltaTime;
		}
	}
}
