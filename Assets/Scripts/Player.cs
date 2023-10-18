using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float turnSmoothVelocity;

    [SerializeField] private Transform cameraPosition;
    [SerializeField] private PlayerInput input;

    // Update is called once per frame
    private void Update() {
        this.MovementHandler();
        this.InteractionHandler();
    }

    private void InteractionHandler () {
        float interactionDistance = 2f;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit interactionObject, interactionDistance)) {
            Debug.Log(interactionObject.transform);
        }
    }
    /*
    TODO: 
    Rigidbody physics is processed within FixedUpdate() => Mixing it with transform.postion modification from Update()
    causes visuals jitters in player's movement. 

    Solution 1: Handle movement in FixedUpdate(). (Not a scalable solution in terms of the frames per second)
    Solution 2: Write a simple gravitational script. 
     */
    private void MovementHandler() {
        float playerHeight = 2f;
        float playerRadius = .7f;
        float moveDistance = moveSpeed * Time.deltaTime;

        // Calculating the direction of movement relative to the camera
        Vector3 inputDirection = input.getInputDirectionNormalized();
        Vector3 camForward = new Vector3(cameraPosition.forward.x, 0, cameraPosition.forward.z);
        Vector3 camRight = new Vector3(cameraPosition.right.x, 0, cameraPosition.right.z);
        Vector3 moveDirection = (inputDirection.z * camForward + inputDirection.x * camRight).normalized;

        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f);
        bool canMove = !Physics.CapsuleCast(
            transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        if (moveDirection.magnitude != 0) {
            transform.eulerAngles = Vector3.up * smoothedAngle;
            transform.position += Convert.ToInt16(canMove) * moveDirection * moveDistance;
        }
    }
}