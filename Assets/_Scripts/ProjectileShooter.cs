using System;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Projectile: πﬂªÁ√º
/// 
/// </summary>
public class ProjectileShooter : MonoBehaviour
{
	public GameObject projectile;

	public Transform spawnPoint;

	void Start()
	{

	}
	private void Update()
	{

	}
	public void Shoot_StraightFWD(float speed)
	{
		GameObject obj = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);

		Rigidbody rb = obj.GetComponent<Rigidbody>();

		if (rb != null)
			rb.linearVelocity = spawnPoint.up * speed;
		//rb.useGravity = true;
	}
	public void Shoot_Parabolic_Projectile(Vector3 target)
	{
		GameObject obj = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
		obj.GetOrAddComponent<Parabolic_Projectile>()?.Init(spawnPoint.position, target);
	}
}
