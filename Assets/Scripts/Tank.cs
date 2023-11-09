using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {
    [SerializeField] private ItemSO tankSO;
    private TankStorage storage;

    public ItemSO getTankSO() {
        return tankSO;
    }

    public void SetStorage(TankStorage storage) {
        if (!storage.isEmpty()) return;
        this.storage?.EjectTank();

        this.storage = storage;
        storage.InjectTank(this);
        transform.parent = storage.GetMountingPoint();
        transform.localPosition = Vector3.zero;
    }

    public TankStorage returnTankStorage() {
        return storage;
    }
}
