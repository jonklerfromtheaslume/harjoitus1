using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Linq;
public enum GuardState { Patrol, Chase, Investigate };

public class Guard : MonoBehaviour {
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float closeEnough = 1f;
    [SerializeField] GuardState currentState;
    [SerializeField] float investigateTime = 2f;
    float searchTimer = 0f;
    SightSensor sensor;
    NavMeshAgent agent;
    int currentTarget = 0;
    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        sensor = GetComponentInChildren<SightSensor>();
        StartTowardsWaypoint();
    }
    void StartTowardsWaypoint() {
        agent.destination = waypoints[currentTarget].position;
    }
    int SelectSmartestWaypoint() {
        int closestFound = 0;
        for (int i = 1; i < waypoints.Count; i++) {
            if (Vector3.Distance(waypoints[i].position, transform.position) <
                Vector3.Distance(waypoints[closestFound].position, transform.position)) {
                closestFound = i;
            }
        }
        int next = closestFound + 1;
        if (next == waypoints.Count)
            next = 0;
        return next;
    }
    Vector3 GetClosestVisiblePlayerPos() {
        // TODO if you want the practice: rewrite with foreach,
        // just like where we search for the closest waypoint
        var visiblePlayers = sensor.GetVisiblePlayers();
        var closestPos =    visiblePlayers.
                            OrderBy(x => Vector3.Distance(transform.position, x.position)).
                            First().position;
        return closestPos;
    }
    void Update() {
        var visiblePlayers = sensor.GetVisiblePlayers();
        if (visiblePlayers.Count > 0) {
            currentState = GuardState.Chase;
            var targetPos = GetClosestVisiblePlayerPos();
            agent.destination = targetPos;
        }

        if (currentState == GuardState.Chase) {
            if (visiblePlayers.Count == 0) {
                currentState = GuardState.Investigate;
                searchTimer = investigateTime;
            }
        }
        if (currentState == GuardState.Patrol) {
            float distance = Vector3.Distance(transform.position, agent.destination);
            if (distance < closeEnough) {
                currentTarget++;
                if (currentTarget == waypoints.Count) {
                    currentTarget = 0;
                }
                StartTowardsWaypoint();
            }
        }
        if (currentState == GuardState.Investigate) {
            searchTimer -= Time.deltaTime;
            if (searchTimer < 0) {
                currentState = GuardState.Patrol;
                currentTarget = SelectSmartestWaypoint();
                StartTowardsWaypoint();
            }
        }
    }
}
