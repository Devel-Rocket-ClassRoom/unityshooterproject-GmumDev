using UnityEngine;

public class Destroyer : MonoBehaviour
{
	public GameObject particle;
	public Destroyer_SO destroyer_SO;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == destroyer_SO.targetTag.ToString())
		{
			var s = collision.gameObject.GetComponent<IDamageable>();

			if (s != null)
			{
				s.Damage(destroyer_SO.damage);
			}
			if (particle != null) 
			{
				Instantiate(particle, transform.position, particle.transform.rotation); 
			}

			if(destroyer_SO.destroyOnCollisionEnter)
			{
				Destroy(gameObject);
			}
		}
	}
	
}
