using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy_Artillery : Enemy
{
	[Header("Fire")]
	private ProjectileShooter projectileShooter;
	public float fireInterval = 1f;
	public float fireChance = 0.2f;
	private float lastFireT = 0f;


	protected override void Start()
	{
		base.Start();

		projectileShooter = GetComponent<ProjectileShooter>();

		lastFireT = 0f;
	}
	void Update()
	{
		Vector3 dv = target.position - transform.position;
		transform.localRotation = Quaternion.LookRotation(new Vector3(dv.x, 0, dv.z));
		if (Time.time - lastFireT > fireInterval)
		{
			lastFireT = Time.time;
			if (Random.value < fireChance)
			{
				if(anim != null)
					anim.SetTrigger("onAttack");
				projectileShooter.Shoot_Parabolic_Projectile(target.position);
			}
		}

		if (anim != null && anim.GetCurrentAnimatorStateInfo(0).IsName("END"))
		{
			if (deathParticle != null)
			{
				Instantiate(deathParticle, transform.position, deathParticle.transform.rotation);
			}
			Destroy(gameObject);
		}
	}

}
