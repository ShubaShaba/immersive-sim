using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFillInStation : MonoBehaviour, IInteractable, IItemCarrier, ShootingTarget {
    // TODO: Create a base class for item carriers and carriable items (possibly)
    [SerializeField] private Transform mountingPoint;
    private Tank tank;
    
    void IInteractable.Interact(Transform interactor) {
        IItemCarrier carrier = null;
        interactor.TryGetComponent(out carrier);
        if (carrier == null) return;

        if (IsEmpty() && !carrier.IsEmpty()) {
            carrier.GetItem().SetParent(this);
        } else if(!IsEmpty() && carrier.IsEmpty()) {
            tank.SetParent(carrier);
        }
    }

    string IInteractable.getInteractHint() {
        return "Fill in the tank";
    }
    
    public Transform GetMountingPoint () {
        return mountingPoint;
    }

    public bool Inject(ICarryableItem item) {
        bool isTank = item is Tank;
        if (isTank && IsEmpty()) tank = (Tank) item;
        return isTank;
    }

    public void Eject() {
        tank = null;
    }

    public bool IsEmpty() {
        return tank == null;
    }

    public ICarryableItem GetItem() {
        return tank;
    }

    void ShootingTarget.OnHit() {
        Debug.Log(gameObject);
    }
}
