using UnityEngine;

public class SelectableCharacter : MonoBehaviour {
    [SerializeField] Color normalColor = Color.blue;
    [SerializeField] Color selectedColor = Color.red;
    [SerializeField] Color highlightColor = Color.white;

    InputManager inputManager;
    Renderer rend;
    public void OnSelect() {
        rend.material.color = selectedColor;
    }
    public void OnDeselect() {
        rend.material.color = normalColor;
    }
    void Awake() {
        inputManager = FindAnyObjectByType<InputManager>();
        rend = GetComponentInChildren<Renderer>();
        rend.material.color = normalColor;
    }
    void OnMouseDown() {
        inputManager.NewSelection(this);
    }
    void OnMouseEnter() {
        if (!inputManager.IsSelected(this)) {
            rend.material.color = highlightColor;
        }    
    }
    void OnMouseExit() {
        if (!inputManager.IsSelected(this)) {
            rend.material.color = normalColor;
        }
    }
}
