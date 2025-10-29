using UnityEngine;

public class Door : MonoBehaviour {
    public bool opening = false;
    [Range(0, 1)] public float howOpen = 0;
    public float speed = 1; // how many percent opening / second
    public float maxRotation = 90f;
    Quaternion initialRot;
    void Awake() {
        initialRot = transform.rotation;
    }
    public void Open() {
        opening = true;
    }
    public void Close() {
        opening = false;
    }
    void Update() {
        howOpen += Time.deltaTime * speed * (opening ? 1 : -1);
        howOpen = Mathf.Clamp01(howOpen);
        var rot = Quaternion.AngleAxis(howOpen * maxRotation, Vector3.up);
        transform.rotation = rot * initialRot;
    }
}
