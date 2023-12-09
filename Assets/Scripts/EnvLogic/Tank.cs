using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    TODO: base carrieable object class
    Need to include Inject() method returns false*
*/
public class Tank : MonoBehaviour, ICarryableItem, IThrowable, ShootingTarget {
    [SerializeField] private ItemSO tankSO;
    private IItemCarrier carrier;
    private Rigidbody rb;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        EnablePhysics(false);
    }

    private void EnablePhysics(bool enable) {
        rb.isKinematic = !enable;
        rb.detectCollisions = enable;
        rb.freezeRotation = !enable;
    }

    public ItemSO getTankSO() {
        return tankSO;
    }

    public void SetCarrier(IItemCarrier carrier) {
        if (!carrier.IsEmpty()) return;
        this.carrier?.Eject();

        this.carrier = carrier;
        carrier.Inject(this);
        EnablePhysics(false);
        transform.parent = carrier.GetMountingPoint();
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void RemoveCarrier() {
        carrier?.Eject();
        carrier = null; 
        transform.parent = null;
        EnablePhysics(true);
    }

    public IItemCarrier ReturnCarrier() {
        return carrier;
    }
    
    void ShootingTarget.OnHit() {
        Debug.Log(gameObject);
    }

    Rigidbody IThrowable.GetRigidbody() {
        return rb;
    }
}
