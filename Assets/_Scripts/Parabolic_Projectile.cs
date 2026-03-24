using UnityEngine;

public class Parabolic_Projectile : MonoBehaviour
{
	public float speed_xz = 5f;
    private Vector3 dir_xz;
    private float duration;


    public void Init(Vector3 startPos, Vector3 targetPos)
    {
		float dy = targetPos.y - startPos.y;

		
        startPos = new Vector3(startPos.x, 0, startPos.z);
		targetPos = new Vector3(targetPos.x, 0, targetPos.z);

		var tmpdir = (targetPos - startPos).normalized;
		dir_xz = new Vector3(tmpdir.x, 0, tmpdir.z);

        duration = Vector3.Distance(startPos, targetPos) / speed_xz;


		float g = -Physics.gravity.magnitude;
		float T = duration;
		float initial_vert_speed = (dy / T) - (0.5f * g * T);
			//g/2.0f*(T - Mathf.Sqrt(T*T - 2 + (4/g*dy)));
			//Mathf.Sqrt(2/(3*duration) *(gravity*duration*duration/2 - dy));
			// gravity * duration / 2;
		
        transform.GetComponent<Rigidbody>().linearVelocity = Vector3.up * initial_vert_speed + dir_xz * speed_xz;



	}
}
