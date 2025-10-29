using UnityEngine;

public class TickDemo : MonoBehaviour {
    [SerializeField] float tickTime = 2f;
    float timer = 0;

    private void Update() {
        timer += Time.deltaTime;
        while (timer > tickTime) {
            print("TICK!");
            // timer = 0;
            timer -= tickTime;
        }
    }
}
