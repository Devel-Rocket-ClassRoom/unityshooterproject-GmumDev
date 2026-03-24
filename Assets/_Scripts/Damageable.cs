using System;
using System.Linq.Expressions;
using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable
{
	public GameObject ouchParticleObj;
	public GameObject deathParticle;

	public float maxHP = 3f;
    public bool destroyOnZeroHP = true;
    protected float curHP;

	public delegate void onDamagedEvent();
	public onDamagedEvent onDamaged;

	public float CurHP { get { return curHP; } }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        curHP = maxHP;
    }
    
    public float Damage(float damage)
	{
		onDamaged.Invoke();
		if (ouchParticleObj != null)
		{
			Instantiate(ouchParticleObj, transform.position, ouchParticleObj.transform.rotation);
		}
		curHP -= damage;
        if (curHP <= 0 && destroyOnZeroHP)
            Die();
        return curHP;
    }
    private void Die()
	{
		if (deathParticle != null)
		{
			Instantiate(deathParticle, transform.position, deathParticle.transform.rotation);
		}
		Destroy(gameObject);
    }
}
