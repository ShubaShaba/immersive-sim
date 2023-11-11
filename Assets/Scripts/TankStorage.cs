using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankStorage : MonoBehaviour, IInteractable {
    [SerializeField] private ItemSO tankSO;
    [SerializeField] private Transform mountingPoint;
    private Tank tank;

    void IInteractable.Interact(Transform interactor) {
        if (tank == null) {
            Transform tankTransform = Instantiate(tankSO.Prefab, mountingPoint);
            tankTransform.GetComponent<Tank>().SetStorage(this);
        } else {
            tank.EjectFromStorage();
        }
    }
    string IInteractable.getInteractHint() {
        return "Get a tank (empty)";
    }
    
    public Transform GetMountingPoint () {
        return mountingPoint;
    }

    public void InjectTank(Tank tank) {
        this.tank = tank;
    }

    public void EjectTank() {
        tank = null;
    }

    public bool isEmpty() {
        return tank == null;
    }
}
