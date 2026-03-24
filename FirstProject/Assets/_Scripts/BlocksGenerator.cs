using System.Collections;
using UnityEngine;

public class BlocksGenerator : MonoBehaviour
{
    [Header("Blocks")]
    public GameObject prefab;
    private float prefab_size_x = 5f;
	private float prefab_size_y = 5f;

    [Header("Blocks align")]
    public Transform parentsTransform;
    public float indent = 1f;
    public int width = 50;
    public int height = 20;

    [Header("Use Animation")]
    public bool isAnimated = false;
    public float animDuration = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        prefab_size_x = prefab.GetComponent<Transform>().lossyScale.x;
		prefab_size_y = prefab.GetComponent<Transform>().lossyScale.y;

		if (isAnimated)
        {
            StartCoroutine(CreateBlocks(prefab_size_x, prefab_size_y));
        }
        else
        {
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    Vector3 positionDelta = new Vector3(
						(-width / 2.0f + j) * prefab_size_x + indent,
                        i * prefab_size_y + indent,
                        0
					);
                    Instantiate(prefab, parentsTransform.localPosition + positionDelta, Quaternion.identity, parentsTransform);
                }
            }
        }
    }
    
    IEnumerator CreateBlocks(float sizex, float sizey)
	{
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				Vector3 positionDelta = new Vector3(
					(-width / 2.0f + j) * prefab_size_x + indent,
					i * prefab_size_y + indent,
					0
				);
				Instantiate(prefab, parentsTransform.localPosition + positionDelta, Quaternion.identity, parentsTransform);

                yield return new WaitForSeconds(animDuration/height/width);
			}
		}
        yield return null;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
