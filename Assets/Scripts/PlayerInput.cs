using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteraction; 

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_called;
    }

    private void Interact_called(UnityEngine.InputSystem.InputAction.CallbackContext context) {
        if (OnInteraction != null) OnInteraction(this, EventArgs.Empty);
    }

    public Vector3 getInputDirectionNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return new Vector3(inputVector.x, 0, inputVector.y).normalized;
    }
}