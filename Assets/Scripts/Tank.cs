using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    TODO: base carrieable object class
    Need to include Inject() method returns false*
*/
public class Tank : MonoBehaviour, ICarryableItem {
    [SerializeField] private ItemSO tankSO;
    private IItemCarrier carrier;

    public ItemSO getTankSO() {
        return tankSO;
    }

    public void SetParent(IItemCarrier carrier) {
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

    public IItemCarrier ReturnParent() {
         return carrier;
    }
}
