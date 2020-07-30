using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : MonoBehaviour
{
    public GameObject[] wayPoints;
    public float speed = 3.0f;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FollowPath();
    }

    int FindTheClosestPoint()
    {
        int waypointIndex = 0;
        for (int i = 0; i < wayPoints.Length; i++)
        {
            //Calculate the distance of enemy and the waypoint and select the closest
            float distance = Vector3.Distance(transform.position, wayPoints[0].transform.position);
            Debug.Log("Distance between = " + distance);
            float minDistance = distance;

            if (distance <= minDistance)
            {
                minDistance = distance;
                waypointIndex = i;
            }
        }
        return waypointIndex;
    }

    void FollowPath()
    {
        int pathIndex = FindTheClosestPoint();

        direction = Vector3.Normalize(wayPoints[pathIndex].transform.position - transform.position);
        // Move toward that direction
        transform.position += direction * Time.deltaTime * speed;

        if (transform.position == wayPoints[pathIndex].transform.position)
        {
            pathIndex = FindTheClosestPoint();
        }
    }
}
