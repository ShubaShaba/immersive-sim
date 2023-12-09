using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThrowable {
    void Throw(Vector3 force) {
        Rigidbody throwable = GetRigidbody();
        if (throwable.isKinematic) {
            Debug.Log("Throwing object is kinematic");
            return;
        }

        // Make sure the object is static and is unattached:
        throwable.velocity = Vector3.zero;
        throwable.angularVelocity = Vector3.zero;
        throwable.transform.SetParent(null, true);

        throwable.AddForce(force, ForceMode.Impulse); 
    }
    protected Rigidbody GetRigidbody();
}
