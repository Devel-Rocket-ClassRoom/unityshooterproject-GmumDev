using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Patrol : MonoBehaviour
{
    [Header("Patrol Properties")]
    public List<Vector3> positions = new List<Vector3>();

    public float patrolSpeed = 4f;

    // isCocktail, then patrol back-and-forth.
	public bool isCocktail = true;

    private int curIdx = 0;
    public bool isRunning = true;
    private bool isCocktail_ascending = true;

    Vector3 dir;
    float distance;
    float lastCornerT;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        Init();
	}
    private void Init()
	{
        if (positions.Count < 2)
        {
            Debug.Log("[Patrol.cs]: Patrol points cannot be less than 2!!");
        }
        else
		{
			curIdx = 0;
			updateDir();
			transform.localPosition = positions[curIdx];
		}
		isRunning = true;
		isCocktail_ascending = true;
	}
    public void SetPositions(Vector3[] positions)
    {
        this.positions.Clear();
		this.positions.AddRange(positions);
        Init();
    }
    // Update is called once per frame
    void Update()
    {
        if(isRunning)
        {
            Update_Patrol();
        }
    }
    private void updateDir()
	{
		distance = Vector3.Distance(positions[curIdx], positions[getNextIdx()]);
		dir = positions[getNextIdx()] - positions[curIdx];
        dir = new Vector3(dir.x, 0, dir.z);
		dir.Normalize();
		lastCornerT = Time.time;
	}
    protected void Update_Patrol()
    {

        if(Time.time - lastCornerT > distance/patrolSpeed)
        {
            curIdx = getNextIdx();
            updateDir();
		}
        
        transform.localRotation = Quaternion.LookRotation(dir);
		transform.localPosition += dir * patrolSpeed * Time.deltaTime;
    }
    private int getNextIdx()
    {
        if(isCocktail)
		{
			if (isCocktail_ascending)
			{
                if(curIdx < positions.Count - 1)
                {
                    return curIdx + 1;
                }
                else
                {
                    isCocktail_ascending = false;
                    return curIdx - 1;
                }
			}
            else
			{
				if (curIdx > 0)
				{
					return curIdx - 1;
				}
                else
                {
                    isCocktail_ascending = true;
                    return curIdx + 1;
                }
			}
		}
        else
		{
			if (curIdx < positions.Count - 1)
			{
				return curIdx + 1;
			}
            else
            {
                return 0;
            }
		}
    }
}
