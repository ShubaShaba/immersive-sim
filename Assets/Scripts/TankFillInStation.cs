using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFillInStation : MonoBehaviour, IInteractable, ItemCarrier {
    // TODO: Create a base class for item carriers and carriable items
    [SerializeField] private Transform mountingPoint;
    private Tank tank;
    
    void IInteractable.Interact(Transform interactor) {
        
    }
    string IInteractable.getInteractHint() {
        return "Fill in the tank";
    }
    
    public Transform GetMountingPoint () {
        return mountingPoint;
    }

    public void Inject(CarryableItem item) {
        tank = (Tank) item;
    }

    public void Eject() {
        tank = null;
    }

    public bool IsEmpty() {
        return tank == null;
    }
}
