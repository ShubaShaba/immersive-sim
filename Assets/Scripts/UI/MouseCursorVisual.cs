using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorVisual : MonoBehaviour {
    [SerializeField] private GameObject aimVisual;
    [SerializeField] private PlayerInput input;
    private bool isAiming = false; 

    private void Start() {
        aimVisual.SetActive(isAiming);

        input.GetPlayersActions().Aim.performed += context => {
            isAiming = !isAiming;
            aimVisual.SetActive(isAiming);
        };
    }
}
