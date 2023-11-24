using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private PlayerInput input;
    private Animator animator;
    private bool thirdPersonCamera = true;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        input.OnAim += SwitchMode; 
    }

    private void SwitchMode(object sender, EventArgs args) {
        if (thirdPersonCamera) {
            animator.Play("Aiming Camera");
        } else {
            animator.Play("Third Person Camera");
        }
        thirdPersonCamera = !thirdPersonCamera;
    }
}
