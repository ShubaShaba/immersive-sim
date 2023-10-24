using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSelectionVisual : MonoBehaviour {
    [SerializeField] private GameObject selectedVisual;

    private void Awake() {
        Hide();
    }
    private void Show() {
        selectedVisual.SetActive(true);
    }
    private void Hide() {
        selectedVisual.SetActive(false);
    }
}
