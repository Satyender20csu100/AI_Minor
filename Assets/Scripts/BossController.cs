using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;

    [SerializeField] float enemySpeed = 2f;

    int waypointIndex = 0;

    private void Start()
    {
        transform.position = waypoints[waypointIndex].position; // Starting enemy position = 1st waypoint.
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; //Start point in the scene
        Gizmos.DrawSphere(waypoints[0].position, 0.5f);

        Gizmos.color = Color.green;
        for (int i = 1; i < waypoints.Count; i++)
        {
            Gizmos.DrawSphere(waypoints[i].position, 0.5f);
        }

        Gizmos.color = Color.blue;
        for (int i = 1; i < waypoints.Count; i++)
        {
            Gizmos.DrawLine(waypoints[i - 1].position, waypoints[i].position);
        }

        Gizmos.color = Color.red; // Finish Point in the scene
        Gizmos.DrawSphere(waypoints[waypoints.Count - 1].position, 0.5f);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].position;
            var move = enemySpeed * Time.deltaTime;

            transform.LookAt(targetPosition);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, move);

            if (Vector3.Distance(transform.position, waypoints[waypointIndex].position) < 0.1f)
            {
                waypointIndex++;
                // StartCoroutine(RotateToWaypoint(waypoints[waypointIndex].position,Vector3.Distance(transform.position,waypoints[waypointIndex].position)/150));
            }
        }
        else
            waypointIndex = 0;

    }
}
