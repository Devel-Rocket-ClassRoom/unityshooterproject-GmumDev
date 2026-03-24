using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class PatrolGenerator : MonoBehaviour
{
	[Header("Patrol Object")]
	public GameObject patrolPrefab;
	public int patrolObjNum = 10;

    [Header("Patrol Positions")]
	public Transform patrolObjParent;
    public Transform patrolPositionsParent;
    public int avgPatrolPositionNum = 3;
    public int maxPatrolPositionNumBias = 1;


    private List<Vector3> patrolPositions = new List<Vector3>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform[] transforms = patrolPositionsParent.GetComponentsInChildren<Transform>().ToArray();
        
        for(int i = 1; i < transforms.Length; i++)
            patrolPositions.Add(transforms[i].localPosition);

        for (int i = 0; i < patrolObjNum; i++)
            Generate();
    }
    public void Generate()
    {
        int n;

        if (maxPatrolPositionNumBias != 0)
            n = Random.Range(avgPatrolPositionNum - maxPatrolPositionNumBias, avgPatrolPositionNum + maxPatrolPositionNumBias + 1);
        else
            n = avgPatrolPositionNum;

        Vector3[] selected = patrolPositions.OrderBy(x => Random.value).Take(n).ToArray();

        var obj = Instantiate(patrolPrefab, Vector3.zero, patrolPrefab.transform.localRotation, patrolObjParent);
        var pat = obj.GetComponent<Patrol>();
        if(pat != null)
            pat.SetPositions(selected);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
