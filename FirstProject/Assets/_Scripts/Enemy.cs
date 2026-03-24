using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Enemy : MonoBehaviour, IDamageable
{
	[Header("Life")]
	public GameObject deathParticle;
	public float maxHP = 3f;
	protected float curHP;

	[Header("Move/Anim")]
	public Animator anim;
	protected static Transform target;

	protected virtual void Start()
	{
		if (target == null)
		{
			target = GameObject.FindGameObjectWithTag("Player")?.transform;
		}
		curHP = maxHP;
	}
	public virtual float Damage(float damage)
	{
		anim?.SetTrigger("onHit");

		curHP -= damage;
		if (curHP <= 0)
			Die();
		return curHP;
	}
	protected virtual void Die()
	{
		tag = EnumTags.Finish.ToString();
		if(anim == null)
		{
			Destroy(gameObject);
		}
		else
			anim.SetTrigger("onDie");
	}

}
