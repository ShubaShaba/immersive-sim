using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseWorldPosition : MonoBehaviour {
    [SerializeField] private Camera aimingCamera;

    private void Update () {
        Ray ray = aimingCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit raycastHit)) {
            transform.position = raycastHit.point;
        }
    }
}
