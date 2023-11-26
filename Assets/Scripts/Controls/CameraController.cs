using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour {
    [SerializeField] private InputActionReference input;
    private Animator animator;
    private bool isThirdPersonCamera = true;

    private void Awake() {
        animator = GetComponent<Animator>();
    }
    private void OnEnable() {
        input.action.Enable();
    }

    private void OnDisable() {
        input.action.Disable();
    }

    private void Start() {
        input.action.performed += SwitchMode;
    }

    private void SwitchMode(InputAction.CallbackContext context) {
        if (isThirdPersonCamera) {
            animator.Play("Aiming Camera");
        } else {
            animator.Play("Third Person Camera");
        }
        isThirdPersonCamera = !isThirdPersonCamera;
    }
}
