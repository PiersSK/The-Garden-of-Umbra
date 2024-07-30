using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public GameObject path;
    public List<Transform> waypoints = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PatrolPath()
    {
        /*if (creature.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 3) 
            {
            if(waypointIndex < creature.path.waypoints.Count - 1)
                waypointIndex++;
            else
                waypointIndex = 0;
            creature.Agent.SetDestination(creature.path.waypoints[waypointIndex].position);
            waitTimer = 0;
            }
        }*/
    }
}
