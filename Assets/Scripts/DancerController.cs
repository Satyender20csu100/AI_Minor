using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DancerController : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;

    [SerializeField] float enemySpeed = 2f;

    int waypointIndex = 1;

    private NavMeshAgent navMeshAgent;

    public Animator dancerAnim;

    private bool isReached = false;

    private void Start()
    {
        //dancerAnim.ResetTrigger("Idle");
        dancerAnim.SetTrigger("Walking");
        transform.position = waypoints[waypointIndex].position; // Starting enemy position = 1st waypoint.

        navMeshAgent.destination = waypoints[waypointIndex].position;
        transform.LookAt(navMeshAgent.destination);
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue; //Start point in the scene
        Gizmos.DrawSphere(waypoints[0].position, 0.5f);

        Gizmos.color = Color.blue;
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
        if (waypointIndex < waypoints.Count - 1)
        {
            if (Vector3.Distance(transform.position, waypoints[waypointIndex].position) < 0.1f && !isReached)
            {
                isReached = true;
                dancerAnim.ResetTrigger("Walking");
                dancerAnim.SetTrigger("Idle");

                waypointIndex++;

                dancerAnim.ResetTrigger("Idle");
                dancerAnim.SetTrigger("Walking");
                navMeshAgent.destination = waypoints[waypointIndex].position;

                transform.LookAt(navMeshAgent.destination);
                isReached = false;

            }
        }
        else
        {
            if (Vector3.Distance(transform.position, waypoints[waypointIndex].position) < 0.1f && !isReached)
            {
                isReached = true;
                dancerAnim.ResetTrigger("Walking");
                dancerAnim.SetTrigger("Dancing");

                StartCoroutine(Wait(5));

                

            }

        }

    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);

        dancerAnim.ResetTrigger("Dancing");
        waypointIndex = 0;

        dancerAnim.SetTrigger("Walking");
        navMeshAgent.destination = waypoints[waypointIndex].position;

        transform.LookAt(navMeshAgent.destination);

        dancerAnim.SetTrigger("Walking");
        isReached = false;
    }
}
