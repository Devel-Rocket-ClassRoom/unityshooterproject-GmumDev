using UnityEngine;

public class ContiuousGenerator : MonoBehaviour
{
    public GameObject prefab;
    public float interval = 1f;

    private float lastSpawnT = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastSpawnT = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastSpawnT > interval)
        {
            lastSpawnT = Time.time;

            Instantiate(prefab, transform.position, transform.rotation, transform);
        }
    }
}
