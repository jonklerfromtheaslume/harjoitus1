using UnityEngine;
using UnityEngine.Events;
public class SimpleTriggerPlate : MonoBehaviour {
    public UnityEvent playerEntered;
    public UnityEvent playerExited;
    void OnTriggerEnter(Collider other) {
        // print("Something entered");
        playerEntered.Invoke();
    }
    void OnTriggerExit(Collider other) {
        // print("Something exited");
        playerExited.Invoke();
    }
}
