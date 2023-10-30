using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    private PlayerInputActions playerInputActions;

    public event EventHandler OnInteraction;
    // TODO: Think how to get rid off player reference
    [SerializeField] private Transform playerPosition;
    private IInteractable selectedInteractable;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_called;
    }
    
    private void Update() {
        InteractableSelectionPlayerPos();   
    }

    private void Interact_called(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        if (OnInteraction != null) OnInteraction(this, EventArgs.Empty);
    }

    private void InteractableSelectionPlayerPos() {
        float interactionDistance = 2f;
        bool inInteractRegion = Physics.Raycast(playerPosition.position, playerPosition.forward, out RaycastHit interactionObject, interactionDistance);

        // Check if the object is within reach and is interactable
        if (inInteractRegion && interactionObject.transform.TryGetComponent(out IInteractable interactable)) {
            selectedInteractable = interactable;
        } else {
            selectedInteractable = null;
        }
    }

    public Vector3 getInputDirectionNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return new Vector3(inputVector.x, 0, inputVector.y).normalized;
    }

    public IInteractable getSelectedInteractable() {
        return selectedInteractable;
    }
}