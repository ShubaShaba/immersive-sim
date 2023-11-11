using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour, CarryableItem {
    [SerializeField] private ItemSO tankSO;
    private ItemCarrier carrier;

    public ItemSO getTankSO() {
        return tankSO;
    }

    public void SetParent(ItemCarrier carrier) {
        if (!carrier.IsEmpty()) return;
        this.carrier?.Eject();

        this.carrier = carrier;
        carrier.Inject(this);
        transform.parent = carrier.GetMountingPoint();
        transform.localPosition = Vector3.zero;
    }

    public void RemoveParent() {
        carrier?.Eject();
        carrier = null; 
        transform.parent = null;
    }

    public ItemCarrier ReturnParent() {
         return carrier;
    }
}
