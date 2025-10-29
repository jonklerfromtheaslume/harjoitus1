using UnityEngine;

public class TestDestruction : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.X))
            Destroy(gameObject);
    }
}
