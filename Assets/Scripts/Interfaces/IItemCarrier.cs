using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemCarrier {
    Transform GetMountingPoint();
    bool Inject(CarryableItem item);
    void Eject();
    bool IsEmpty();
    CarryableItem GetItem();
}
