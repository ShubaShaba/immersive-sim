using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseWorldPosition : MonoBehaviour {
    [SerializeField] private PlayerInput input;
    [SerializeField] private GameObject aimVisual;
    [SerializeField] private Camera aimingCamera;

    private bool isAiming = false; 
    
    // TODO: Separate visuals from control logic
    private void Start() {
        aimVisual.SetActive(isAiming);

        input.GetPlayersActions().Aim.performed += context => {
            isAiming = !isAiming;
            aimVisual.SetActive(isAiming);
        };
    }

    private void Update () {
        Ray ray = aimingCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit raycastHit)) {
            transform.position = raycastHit.point;
        }
    }
}
