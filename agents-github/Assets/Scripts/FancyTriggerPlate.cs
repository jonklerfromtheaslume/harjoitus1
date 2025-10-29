using UnityEngine;
using UnityEngine.Events;
public class FancyTriggerPlate : MonoBehaviour {
    // bonus: use Physics.OverlapBox to also count
    // players that are already on the plate at Start!
    
    public UnityEvent plateDown;
    public UnityEvent plateUp;
    public Transform graphics;
    public float shiftY = 0.1f;
    int playersOnPlate = 0;

    void OnTriggerEnter(Collider other) {
        var player = other.GetComponent<SelectableCharacter>();
        if (player != null) {
            playersOnPlate++;
            if (playersOnPlate == 1) {
                plateDown.Invoke();
                graphics.position -= Vector3.up * shiftY;
            }
        }
    }
    void OnTriggerExit(Collider other) {
        var player = other.GetComponent<SelectableCharacter>();
        if (player != null) {
            playersOnPlate--;
            if (playersOnPlate == 0) {
                plateUp.Invoke();
                graphics.position += Vector3.up * shiftY;
            }
        }
    }
}
