using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ItemCarrier {
    Transform GetMountingPoint();
    void Inject(CarryableItem item);
    void Eject();
    bool IsEmpty();
}
