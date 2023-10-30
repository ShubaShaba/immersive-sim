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

    private void InteractableSelectionPosBased(Transform interactionTransform) {
        float interactionDistance = 2f;
        bool interactionWithinReach = Physics.Raycast(interactionTransform.position, interactionTransform.forward, out RaycastHit interactionObject, interactionDistance);
        Transform interactableTransform = interactionWithinReach ? interactionObject.transform : null;

        SelectInteractable(interactableTransform);
        SelectInteractableVisual(interactableTransform);
    }

    private void SelectInteractable(Transform interactionTransform) {
        if (interactionTransform == null) {
            selectedInteractable = null;
            return;
        }
        interactionTransform.TryGetComponent(out IInteractable interactable);
        selectedInteractable = interactable;
    }

    private void SelectInteractableVisual(Transform interactionTransform) {
        selectedInteractableVisual?.Notify(selectedInteractable);
        if (interactionTransform == null) {
            selectedInteractableVisual = null;
            return;
        }
        interactionTransform.TryGetComponent(out InteractableSelectionVisual interactableVisual);
        selectedInteractableVisual = interactableVisual;
        selectedInteractableVisual?.Notify(selectedInteractable);
    }

    public Vector3 GetInputDirectionNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return new Vector3(inputVector.x, 0, inputVector.y).normalized;
    }

    public IInteractable GetSelectedInteractable() {
        return selectedInteractable;
    }
}