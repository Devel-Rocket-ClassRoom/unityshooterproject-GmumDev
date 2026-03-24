using UnityEngine;

public class Enemy_Patroler : Enemy
{
	[Header("Fire")]
	private ProjectileShooter projectileShooter;
	public float fireInterval = 2f;
	public float fireChance = 0.3f;
	private float lastFireT = 0f;
	public float projectileSpeed = 22f;

	[Header("Move/Anim")]
	public float runSpeed = 4f;
	private Patrol patrol;


	protected override void Start()
	{
		base.Start();

		projectileShooter = GetComponent<ProjectileShooter>();
		patrol = GetComponent<Patrol>();

		lastFireT = 0f;
	}
	void Update()
	{
		if (patrol.isRunning == false)
		{
			transform.LookAt(target);
			transform.position += transform.forward * runSpeed * Time.deltaTime;
			if (Time.time - lastFireT > fireInterval)
			{
				lastFireT = Time.time;
				if (Random.value < fireChance)
				{

					if (anim != null)
						anim.SetTrigger("onAttack");
					projectileShooter.Shoot_StraightFWD(projectileSpeed);
				}
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

	public override float Damage(float damage)
	{
		patrol.isRunning = false;
		return base.Damage(damage);
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == EnumTags.Player.ToString())
		{
			anim?.SetTrigger("onSurprised");
			patrol.isRunning = false;
		}
	}
}
