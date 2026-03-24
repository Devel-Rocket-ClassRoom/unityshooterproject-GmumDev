using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class ConsoleGamepadButtons : MonoBehaviour
{
	// 5 6 4 3 2
	// L R Y B A
	ReadOnlyArray<InputAction> actions;

	void Start()
	{
		actions = InputSystem.actions.FindActionMap("Player").actions;
		foreach(var action in actions)
		{
			action.performed += (x) => Debug.Log(x);
		}
	}
    // Update is called once per frame
    void Update()
	{

	}
}
