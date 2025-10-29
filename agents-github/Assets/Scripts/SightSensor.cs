using UnityEngine;
using System.Collections.Generic;

public class SightSensor : MonoBehaviour {
    [SerializeField] float visionRange = 5f;
    [SerializeField] float visionAngle = 45f;
    [SerializeField] List<Transform> visiblePlayers;
    [SerializeField] LayerMask visionBlockersMask;
    List<Transform> playerCharacters = new List<Transform>();
    public List<Transform> GetVisiblePlayers() {
        return visiblePlayers;
    }
    void FindPlayerCharacters() {
        var players = FindObjectsByType<SelectableCharacter>(FindObjectsSortMode.None);
        foreach (var player in players) {
            playerCharacters.Add(player.transform);
        }
    }
    void Start() {
        FindPlayerCharacters();
    }
    void UpdateVisiblePlayers() {
        visiblePlayers.Clear();
        foreach (var player in playerCharacters) {
            Vector3 pos = player.position;
            // check if player close enough, if not, check next player
            float dist = Vector3.Distance(transform.position, pos);
            if (dist > visionRange)
                continue; // skip to next iteration of loop
            // check if player in front of sensor, if not, check next player
            var delta = pos - transform.position;
            float angle = Vector3.Angle(transform.forward, delta);
            if (angle > visionAngle)
                continue;
            // check if player not obscured by wall
            // float playerDistance = Vector3.Distance(transform.position, pos);
            // float playerDistance = delta.magnitude;
            if (Physics.Raycast(transform.position,
                                delta,
                                out RaycastHit hitInfo,
                                delta.magnitude,
                                visionBlockersMask)) {
                Debug.DrawLine(transform.position, hitInfo.point, Color.red);
                continue;
            } else {
                Debug.DrawLine(transform.position, pos, Color.white);
            }
            visiblePlayers.Add(player);
        }
    }
    void OnDrawGizmos() { // OnDrawGizmosSelected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
    void Update() {
        UpdateVisiblePlayers();
        // draw vision angle lines for debugging
        var clockwise = Quaternion.Euler(0, -visionAngle, 0);
        var counterClockwise = Quaternion.Euler(0, visionAngle, 0);
        var longForward = transform.forward * visionRange;
        var left = counterClockwise * longForward;
        var right = clockwise * longForward;
        var p = transform.position;
        Debug.DrawLine(p, p + left);
        Debug.DrawLine(p, p + right);
    }
}
