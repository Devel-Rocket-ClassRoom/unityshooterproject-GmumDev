using UnityEngine;

public class FaceToParent : MonoBehaviour
{
    private static Transform target;
    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }
}
    