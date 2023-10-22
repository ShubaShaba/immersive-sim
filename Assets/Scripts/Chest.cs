using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable {
    void IInteractable.Interact(Transform interactor) {
        Debug.Log(GetComponent<Transform>());
    }
    string IInteractable.getInteractHint() {
        return "Open chest";
    } 
}
