using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
public class WaypointPatroller : MonoBehaviour {
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float closeEnough = 1f;
    NavMeshAgent agent;
    int currentTarget = 0;
    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        StartTowardsWaypoint();
    }
    void StartTowardsWaypoint() {
        agent.destination = waypoints[currentTarget].position;
    }
    void Update() {
        float distance = Vector3.Distance(transform.position, agent.destination);
        if (distance < closeEnough) {
            currentTarget++;
            if (currentTarget == waypoints.Count) {
                currentTarget = 0;
            }
            StartTowardsWaypoint();
        }
    }
}
