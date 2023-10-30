using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    private PlayerInputActions playerInputActions;
    // TODO: Think how to get rid off player reference
    [SerializeField] private Transform playerPosition;

    private IInteractable selectedInteractable;
    private InteractableSelectionVisual selectedInteractableVisual;
    public event EventHandler OnInteraction;
    
    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_called;
    }
    
    private void Update() {
        InteractableSelectionPosBased(playerPosition);   
    }

    private void Interact_called(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        if (OnInteraction != null) OnInteraction(this, EventArgs.Empty);
    }

    // TODO: Separate the visuals from selection logic
    private void InteractableSelectionPosBased(Transform interactionTransform) {
        float interactionDistance = 2f;
        bool inInteractRegion = Physics.Raycast(interactionTransform.position, interactionTransform.forward, out RaycastHit interactionObject, interactionDistance);

        // Check if the object is within reach and is interactable
        if (inInteractRegion && interactionObject.transform.TryGetComponent(out IInteractable interactable) && interactionObject.transform.TryGetComponent(out InteractableSelectionVisual interactableVisual)) {
            selectedInteractable = interactable;
            selectedInteractableVisual?.Notify(selectedInteractable);
            selectedInteractableVisual = interactableVisual;
        } else {
            selectedInteractable = null;
            selectedInteractableVisual?.Notify(selectedInteractable);
            selectedInteractableVisual = null;
        }
        selectedInteractableVisual?.Notify(selectedInteractable);
    }

    public Vector3 getInputDirectionNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return new Vector3(inputVector.x, 0, inputVector.y).normalized;
    }

    public IInteractable getSelectedInteractable() {
        return selectedInteractable;
    }
}