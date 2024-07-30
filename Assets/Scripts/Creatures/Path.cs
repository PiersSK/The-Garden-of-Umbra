using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    private float waitTimer;

    public GameObject path;
    public List<Transform> waypoints = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        waitTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PatrolPath(Creatures creature, int waypointIndex)
    {
        if (creature.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 3) 
            {
                if(waypointIndex < waypoints.Count - 1)
                {
                    waypointIndex++;
                }
                else
                {
                    waypointIndex = 0;
                }
                
            creature.Agent.SetDestination(waypoints[waypointIndex].position);
            waitTimer = 0;
            }
        }
    }
}
