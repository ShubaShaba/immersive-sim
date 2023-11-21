using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorldPosition : MonoBehaviour {
    [SerializeField] private Camera thirdPersonCamera;

    private void Update () {
        Ray ray = thirdPersonCamera.ScreenPointToRay(UnityEngine.InputSystem.Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit raycastHit)) {
            transform.position = raycastHit.point;
        }
    }
}
