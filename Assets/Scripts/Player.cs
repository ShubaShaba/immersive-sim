using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float turnSmoothVelocity;

    [SerializeField] private Transform cameraPosition;
    [SerializeField] private PlayerInput input;

    // Update is called once per frame
    private void Update() {
        Vector3 inputDirection = input.getInputDirectionNormalized();

        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraPosition.eulerAngles.y;
        float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f);

        if (inputDirection.magnitude != 0) {
            transform.eulerAngles = Vector3.up * smoothedAngle;
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
        }
    }
}