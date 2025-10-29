using UnityEngine;
using UnityEngine.AI;

public class InputManager : MonoBehaviour {
    //[SerializeField] 
    public SelectableCharacter currentSelection;
    public LayerMask groundOnly;
    Camera cam;
    public bool IsSelected(SelectableCharacter character) {
        return character == currentSelection;
    }
    public void NewSelection(SelectableCharacter newSelection) {
        if (currentSelection != null) {
            currentSelection.OnDeselect();
        }
        newSelection.OnSelect();
        currentSelection = newSelection;
    }
    void Awake() {
        cam = Camera.main;
    }
    void Update() {
        if (currentSelection != null) {
            var t = currentSelection.transform;
            if (Input.GetKeyDown(KeyCode.Mouse1)) {
                var agent = currentSelection.GetComponent<NavMeshAgent>();
                var ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundOnly)) {
                    agent.destination = hit.point;
                    //t.position = hit.point;
                }
            }
        }
    }

}
