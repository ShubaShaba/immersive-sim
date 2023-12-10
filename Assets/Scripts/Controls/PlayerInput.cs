using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    private PlayerInputActions playerInputActions;
    [SerializeField] private Player player;
    [SerializeField] private Transform cursorPosition;
    private InteractableSelectionVisual selectedInteractableVisual;
    public static IInteractable selectedInteractable { get; private set; }
    private PlayerInputActions.PlayerActions playersActions;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playersActions = playerInputActions.Player;
        playerInputActions.Player.Enable();
    }
    
    private void Update() {
        InteractableSelectionPosBased(player.transform);
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

    public PlayerInputActions.PlayerActions GetPlayersActions() {
        return playersActions; 
    }

    public Vector3 GetCursorPosition() {
        return cursorPosition.position;
    }

    public Player GetPlayer() {
        return player;
    }
}