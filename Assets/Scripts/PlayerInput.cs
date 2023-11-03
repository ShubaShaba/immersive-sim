using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    private PlayerInputActions playerInputActions;
    // TODO (Optional): Think how to get rid of player reference
    [SerializeField] private Transform playerPosition;

    // TODO (Optional): Think how to get rid of singelton pattern usage, is it applicable ?
    public static IInteractable selectedInteractable { get; private set; }
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

    // Selects an interactable object based on current Player's position
    private void InteractableSelectionPosBased(Transform interactionTransform) {
        float interactionDistance = 2f;
        bool interactionWithinReach = Physics.Raycast(interactionTransform.position, interactionTransform.forward, out RaycastHit interactionObject, interactionDistance);
        Transform interactableTransform = interactionWithinReach ? interactionObject.transform : null;

        SelectInteractable(interactableTransform);
        SelectInteractableVisual(interactableTransform);
    }

    private void SelectInteractable(Transform interactableTransform) {
        if (interactableTransform == null) {
            selectedInteractable = null;
        } else {
            interactableTransform.TryGetComponent(out IInteractable interactable);
            selectedInteractable = interactable;
        }
    }

    /* Updates the reference to the selected interactable's visual and notifies it
     * In case the selected interactable has changed, both new and previous visuals get notification
     */
    private void SelectInteractableVisual(Transform interactableTransform) {
        selectedInteractableVisual?.Notify();
        if (interactableTransform == null || !interactableTransform.TryGetComponent(out InteractableSelectionVisual interactableVisual)) {
            selectedInteractableVisual = null;
        } else {
            selectedInteractableVisual = interactableVisual;
            selectedInteractableVisual.Notify();
        }
    }

    public Vector3 GetInputDirectionNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return new Vector3(inputVector.x, 0, inputVector.y).normalized;
    }
}