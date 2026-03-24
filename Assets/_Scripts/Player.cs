using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	[Header("Fire")]
	public float projectileSpeed = 22f;
	public float fireRate = 3f;
	private ProjectileShooter projectileShooter;
	private InputAction fireAction;
	private float lastFireT = 0f;

	private Damageable damageable;
	private Animator anim;

	private void Die()
	{
		Debug.Log("Player Dead");
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		projectileShooter = GetComponent<ProjectileShooter>();
		fireAction = InputSystem.actions.FindAction("Fire");
		damageable = GetComponent<Damageable>();
		damageable.onDamaged += () => anim.SetTrigger("Ouch");

		anim = GetComponent<Animator>();
		lastFireT = 0f;
	}


	// Update is called once per frame
	void Update()
	{
		if(damageable.CurHP <= 0)
		{
			Debug.Log("Dead");
		}
		if (fireAction.IsPressed() && Time.time - lastFireT > 1 / fireRate)
		{
			lastFireT = Time.time;
			projectileShooter.Shoot_StraightFWD(projectileSpeed);
		}
	}
}
