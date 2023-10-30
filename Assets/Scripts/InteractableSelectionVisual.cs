using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSelectionVisual : MonoBehaviour {
    private IInteractable interactable;
    [SerializeField] private GameObject selectedVisual;

    private void Awake() {
        if (!TryGetComponent(out IInteractable interactable)) {
            throw new MissingComponentException("No IInteractable component, when trying to apply visuals");
        }
        this.interactable = interactable;
        Hide();
    }
    private void Show() {
        selectedVisual.SetActive(true);
    }
    private void Hide() {
        selectedVisual.SetActive(false);
    }
    public void Notify(IInteractable selectedInteractable) {
        if (selectedInteractable == interactable) {
            Show();
        } else {
            Hide();
        }
    }
}
