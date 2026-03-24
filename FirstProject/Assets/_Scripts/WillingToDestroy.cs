using UnityEngine;

public class WillingToDestroy : MonoBehaviour
{
    public float time = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, time);
    }
}
