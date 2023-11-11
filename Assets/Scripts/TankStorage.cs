using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankStorage : MonoBehaviour, IInteractable, ItemCarrier {
    [SerializeField] private ItemSO tankSO;
    [SerializeField] private Transform mountingPoint;
    private Tank tank;

    void IInteractable.Interact(Transform interactor) {
        if (tank == null) {
            Transform tankTransform = Instantiate(tankSO.Prefab, mountingPoint);
            tankTransform.GetComponent<Tank>().SetParent(this);
        } else {
            tank.RemoveParent();
        }
    }
    string IInteractable.getInteractHint() {
        return "Get a tank (empty)";
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
